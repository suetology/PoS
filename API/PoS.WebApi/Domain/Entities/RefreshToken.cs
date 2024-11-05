using PoS.WebApi.Domain.Common;

namespace PoS.WebApi.Domain.Entities;

public class RefreshToken : Entity
{
    public string Token { get; set; }
    
    public DateTime Expires { get; set; }
    
    public Guid UserId { get; set; }
    
    public User User { get; set; }
}