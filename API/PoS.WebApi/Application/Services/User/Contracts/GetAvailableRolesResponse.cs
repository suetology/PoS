namespace PoS.WebApi.Application.Services.User.Contracts;

public class GetAvailableRolesResponse
{
    public IEnumerable<string> Roles { get; set; }
}