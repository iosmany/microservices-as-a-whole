
namespace Microservices.Base.Commands;


public interface ICommandProcessor
{
}

public interface ICommandProcessor<O> : ICommandProcessor where O: IOrder
{
    Task ExecuteAsync(O order, CancellationToken cancellationToken);
}
