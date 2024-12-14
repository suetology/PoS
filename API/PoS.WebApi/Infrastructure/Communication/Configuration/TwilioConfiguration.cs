namespace PoS.WebApi.Infrastructure.Communication.Configuration;

public class TwilioConfiguration
{
    public string AuthToken { get; set; }

    public string AccountSid { get; set; }

    public string MessagingServiceSid { get; set; }
}