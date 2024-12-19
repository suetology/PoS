namespace PoS.WebApi.Application.Services.Reservation.Exceptions;

public class TimeNotAvailableException : Exception
{
    public TimeNotAvailableException(string message) : base(message)
    {
    }
}