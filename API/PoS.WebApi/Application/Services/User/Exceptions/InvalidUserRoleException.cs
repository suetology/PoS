namespace PoS.WebApi.Application.Services.Order.Exceptions;

public class InvalidUserRoleException : Exception
{
    public InvalidUserRoleException(string message) : base(message)
    {
        
    }
}
