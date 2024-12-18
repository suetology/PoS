namespace PoS.WebApi.Application.Services.Refund.Contracts;

public class GetAllRefundsResponse
{
    public IEnumerable<RefundDto> Refunds { get; set; }
}