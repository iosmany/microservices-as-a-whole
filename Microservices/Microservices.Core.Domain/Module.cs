using Microsoft.Extensions.DependencyInjection;
using NLog;

namespace Microservices.Core.Domain;

public static class Module
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    
    public static void Initialize()
    {
        //masstransit init


        //application init



    }


    public static void Finalize()
    {
        //TODO: Add finalization logic here
    }
}
