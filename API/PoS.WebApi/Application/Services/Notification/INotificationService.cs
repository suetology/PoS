
namespace PoS.WebApi.Application.Services.Notification;

public interface INotificationService {
    Task SendSMS(string message, string phoneNumber);
}