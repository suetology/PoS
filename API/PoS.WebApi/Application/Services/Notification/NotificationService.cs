
namespace PoS.WebApi.Application.Services.Notification;

using Amazon;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;

public class NotificationService : INotificationService
{
    private readonly AmazonSimpleNotificationServiceClient _snsClient = new(
        "AKIAU5LH57GNTIOC5ALU",
        "doEeIxG0m6Vi2tmQNf1qmE1URP+7UM665MWuapvY",
        RegionEndpoint.EUNorth1
    );
    
    public async Task SendSMS(string message, string phoneNumber)
    {
        var SMSRequest = new PublishRequest
        {
            Message = message,
            PhoneNumber = phoneNumber,
        };

        await _snsClient.PublishAsync(SMSRequest);
    }
}
