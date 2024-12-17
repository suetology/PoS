namespace PoS.WebApi.Application.Repositories;

using PoS.WebApi.Domain.Common;
using PoS.WebApi.Domain.Entities;

public interface IPaymentRepository : IRepository<Payment>
{
    Task<Payment> GetByCheckoutSessionId(string checkoutSessionId);
}