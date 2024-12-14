
namespace PoS.WebApi.Application.Services.Notification;

using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;

public class NotificationService : INotificationService
{
    private readonly IAmazonSimpleNotificationService _snsClient;
    
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