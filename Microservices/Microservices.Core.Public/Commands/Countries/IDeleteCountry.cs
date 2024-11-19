using Microservices.Base.Commands;

namespace Microservices.Core.Public.Messages.Countries
{
    public interface IDeleteCountry: IDeleteOrder<long>
    {
    }

    public class DeleteCountry: DeleteOrderBase<long>, IDeleteCountry
    {
    }
}
