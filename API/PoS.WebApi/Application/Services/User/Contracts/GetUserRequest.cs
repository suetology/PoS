namespace PoS.WebApi.Application.Services.User.Contracts;

public class GetUserRequest
{
    public Guid Id { get; set; }
    
    public Guid BusinessId { get; set; }
}