namespace PoS.WebApi.Infrastructure.Security.Exceptions;

public class ExpiredRefreshTokenException : Exception
{
    public ExpiredRefreshTokenException(string message) : base(message)
    {
    }
}