namespace Microservices.Core.Public.Messages.Countries;

using Base.Commands;
using Core.Public.DTO;

[Exchange(Exchanges.ReceiveOrder)]
public interface ICreateCountry : ISaveOrder<ICountry>
{
}

public class CreateCountry : SaveOrderBase<ICountry>, ICreateCountry
{
}