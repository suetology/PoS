namespace PoS.WebApi.Application.Services.Order.Exceptions;

public class InvalidOrderStateException : Exception
{
    public InvalidOrderStateException(string message) : base(message)
    {

    }
}
