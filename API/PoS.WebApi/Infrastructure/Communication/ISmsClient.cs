namespace PoS.WebApi.Infrastructure.Communication;

public interface ISmsClient
{
    Task SendSms(string recepientNumber, string message);
}