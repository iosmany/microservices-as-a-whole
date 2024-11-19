
using Microservices.Base;
using Microservices.Base.Domain;

namespace Microservices.Core.Domain.Domain.Entities;

internal class Currency : Entity
{
    public static readonly Currency usd = new Currency("USD", "US Dollar");
    public static readonly Currency eur = new Currency("EUR", "Chemistry");

    public string Code { get; private set; }
    public string Name { get; private set; }

    public readonly Currency[] AllCurrencies = { usd, eur };
    
    protected Currency()
    {
    }

    public Currency(string code, string name)
    {
        Code = code;
        Name = name;
    }


    public Currency? FromCode(string code)
        => AllCurrencies.FirstOrDefault(c => c.Code == code);

    public static Either<IError, Currency> Create(string code, string name)
    {
        if(string.IsNullOrWhiteSpace(code))
            return Left(ErrorFactory.New("Currency code is required"));

        if (string.IsNullOrWhiteSpace(name))
            return Left(ErrorFactory.New("Currency name is required"));

        return new Currency
        {
            Code = code,
            Name = name
        };
    }
}

