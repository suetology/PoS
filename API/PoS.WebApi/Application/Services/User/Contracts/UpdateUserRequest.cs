using PoS.WebApi.Domain.Enums;
using System.Text.Json.Serialization;

namespace PoS.WebApi.Application.Services.User.Contracts;

public class UpdateUserRequest
{
    [JsonIgnore]
    public Guid BusinessId { get; set; }

    [JsonIgnore]
    public Guid Id { get; set; }

    public string Username { get; set; }

    public string PasswordHash { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public Role Role { get; set; }
}