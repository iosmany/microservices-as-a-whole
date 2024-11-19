using Microservices.Base.Commands;
using Microservices.Core.Public.Messages.Countries;

namespace Microservices.Core.Domain.Services.Countries;

sealed class CreateCountryOrder : ICommandProcessor<ICreateCountry>
{
    public CreateCountryOrder()
    {
    }

    public Task ExecuteAsync(ICreateCountry order, CancellationToken cancellationToken)
    {


        
        return Task.CompletedTask;
    }
}
