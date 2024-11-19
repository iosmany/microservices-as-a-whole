using Microservices.Base.Commands;
using Microservices.Core.Public.DTO;

namespace Microservices.Core.Public.Messages.Countries
{
    public interface IUpdateCountry: ISaveOrder<ICountry>
    {
    }

    public class UpdateCountry : SaveOrderBase<ICountry>, IUpdateCountry
    {
    }
}
