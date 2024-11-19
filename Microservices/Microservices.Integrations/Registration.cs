using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Microservices.Integrations.Masstransit;

public static class Module
{
    public static void Initalize(IEnumerable<Assembly> assemblies, IServiceCollection serviceColletion) 
    {
        var provider = new ConsumerLoggerProvider();
        serviceColletion.AddMassTransit(brCfg => {

            provider.Execute(assemblies)
                    .ToList()
                    .ForEach((consumerType) => {
                        brCfg.AddConsumer(consumerType.impl);
                    });

            brCfg.UsingRabbitMq((context, cfg) =>
            {
                cfg.ConfigureEndpoints(context);

            });
        });
    }
}
