using Microsoft.EntityFrameworkCore;
using PoS.WebApi.Application.Repositories;
using PoS.WebApi.Domain.Entities;
using PoS.WebApi.Infrastructure.Persistence;

namespace PoS.WebApi.Infrastructure.Repositories;

public class PaymentRepository : IPaymentRepository
{
    private readonly DatabaseContext _dbContext;

    public PaymentRepository(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Create(Payment payment)
    {
        await _dbContext.Payments.AddAsync(payment);
    }

    public async Task<Payment> Get(Guid id)
    {
        return await _dbContext.Payments.FindAsync(id);
    }

    public async Task<IEnumerable<Payment>> GetAll()
    {
        return await _dbContext.Payments.ToListAsync();
    }

    public async Task Update(Payment payment)
    {
        _dbContext.Payments.Update(payment);

        await Task.CompletedTask;
    }
}