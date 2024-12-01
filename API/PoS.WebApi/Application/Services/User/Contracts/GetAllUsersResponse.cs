namespace PoS.WebApi.Application.Services.User.Contracts;

public class GetAllUsersResponse
{
    public IEnumerable<UserDto> Users { get; set; }
}