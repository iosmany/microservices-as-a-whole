
namespace Microservices.Base;

public static class ErrorFactory
{
    public static IError New(string message)
    {
        return new Error(message);
    }

    private class Error : IError
    {
        public string Field { get; }
        public string Message { get; }
        public Error(string message, string field=null)
        {
            Field = field;
            Message = message;
        }
    }
}

public interface IError
{
    string Field { get; }
    string Message { get; }

}
