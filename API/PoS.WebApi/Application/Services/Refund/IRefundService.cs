using PoS.WebApi.Application.Services.Refund.Contracts;

namespace PoS.WebApi.Application.Services.Refund;

public interface IRefundService
{
    Task CreateRefund(CreateRefundRequest request);

    Task<GetAllRefundsResponse> GetAllRefunds(GetAllRefundsRequest request);
}