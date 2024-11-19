using MassTransit;
using NLog;
using System.Data;

namespace Microservices.Integrations.Masstransit.Consumers
{
    using Microservices.Base.Commands;
    using Microservices.Base.Persistence;
    using Microsoft.Extensions.DependencyInjection;
    using System.Reflection;

    abstract class ConsumerBase<Cmd> : IConsumer<Cmd> 
        where Cmd : class, IOrder
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        readonly protected IServiceProvider _serviceProvider;
        readonly Func<IDatabaseService, IDbTransaction> _getTransaction;
        protected ConsumerBase(IServiceProvider serviceProvider, Func<IDatabaseService, IDbTransaction> getTransaction)
        {
            _serviceProvider = serviceProvider;
            _getTransaction = getTransaction;
        }

        public async Task Consume(ConsumeContext<Cmd> context)
        {
            using (_serviceProvider.CreateScope())
            {
                var _databaseService = _serviceProvider.GetService<IDatabaseService>();
                if (_databaseService is null)
                    throw new ArgumentNullException(nameof(IDatabaseService));

                await using (var transaction = _getTransaction(_databaseService))
                {
                    var cancellationToken = context.CancellationToken;
                    try
                    {
                        ICommandProcessor<Cmd>? procesor = _serviceProvider.GetService<ICommandProcessor<Cmd>>();
                        if (procesor is null)
                            throw new ArgumentNullException();

                        await procesor.ExecuteAsync(context.Message, cancellationToken);
                        cancellationToken.ThrowIfCancellationRequested();

                        await transaction.CommitAsync();
                    }
                    catch (DBConcurrencyException ce)
                    {
                        logger.Error(ce);
                        await transaction.RollbackAsync();
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex);
                        await transaction.RollbackAsync();
                    }
                }
            }
        }
    }

    class OptimisticConsumer<Cmd> : ConsumerBase<Cmd>, IConsumer<Cmd> 
        where Cmd : class, IOrder
    {
        public OptimisticConsumer(IServiceProvider serviceProvider) 
            : base(serviceProvider, (d)=> d.BeginTransaction(IsolationLevel.ReadCommitted))
        {
        }
    }

    class TransactionalConsumer<Cmd> : ConsumerBase<Cmd>, IConsumer<Cmd>
         where Cmd : class, IOrder
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        public TransactionalConsumer(IServiceProvider serviceProvider)
             : base(serviceProvider, (d) => d.BeginTransaction(IsolationLevel.ReadCommitted))
        {
        }
    }

    sealed class OrderConsumerFactory
    {
        public OrderConsumerFactory() 
        { 
        }

        public (Type typo, Type impl) Create(Type cmd, IsolationLevel isolationLevel)
        {
            var consumerInt = typeof(IConsumer<>).MakeGenericType(cmd);

            var consumerImpl = isolationLevel switch {
                IsolationLevel.ReadCommitted => typeof(OptimisticConsumer<>).MakeGenericType(cmd),
                _ => typeof(TransactionalConsumer<>).MakeGenericType(cmd)
            };

            return (consumerInt, consumerImpl);
        }
    }

    public class SubmitOrderConsumerDefinition<C> : ConsumerDefinition<C> where C: class, IConsumer
    {
        readonly ExchangeAttribute? _exchangeAttribute;
        public SubmitOrderConsumerDefinition()
        {
            var type = typeof(C);   
            _exchangeAttribute = type.GetCustomAttribute<ExchangeAttribute>();

            // override the default endpoint name, for whatever reason
            EndpointName = _exchangeAttribute?.ExchangeName ?? "";
            // limit the number of messages consumed concurrently
            // this applies to the consumer only, not the endpoint
            ConcurrentMessageLimit = 4;
        }

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, 
            IConsumerConfigurator<C> consumerConfigurator, 
            IRegistrationContext context)
        {

            endpointConfigurator.UseMessageRetry(r => r.Immediate(3));

            base.ConfigureConsumer(endpointConfigurator, consumerConfigurator, context);
        }

    }
}

