using PoS.WebApi.Infrastructure.Communication.Configuration;
using Twilio;
using Twilio.Rest.Conversations.V1.Service.Conversation;
using Twilio.Types;

namespace PoS.WebApi.Infrastructure.Communication;

public class TwilioSmsClient : ISmsClient
{
    private readonly TwilioConfiguration _twilioConfiguration;

    public TwilioSmsClient(TwilioConfiguration twilioConfiguration)
    {
        _twilioConfiguration = twilioConfiguration;
    }

    public async Task SendSms(string recepientNumber, string message)
    {
        TwilioClient.Init(_twilioConfiguration.AccountSid, _twilioConfiguration.AuthToken);
        
        /*var messageOptions = new CreateMessageOptions(
            new PhoneNumber(recepientNumber));
            messageOptions.MessagingServiceSid = "MG73e7c788d3a492080890f7a91d4ff7e5";
            
        messageOptions.Body = message;*/
        
        //await MessageResource.CreateAsync(messageOptions);
    }
}