using PoS.WebApi.Domain.Common;

namespace PoS.WebApi.Domain.Entities;

public class RefreshToken : Entity
{
    public string AccessToken { get; set; }

    public DateTime Expires { get; set; }
}