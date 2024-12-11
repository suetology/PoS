
namespace PoS.WebApi.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;
using PoS.WebApi.Application.Repositories;
using PoS.WebApi.Application.Services.Order;
using PoS.WebApi.Domain.Entities;
using PoS.WebApi.Domain.Enums;
using PoS.WebApi.Infrastructure.Persistence;

public class OrderRepository : IOrderRepository
{
    private readonly DatabaseContext _dbContext;

    public OrderRepository(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task Create(Order order)
    {
        await _dbContext.Orders.AddAsync(order);
    }

    public async Task<Order> Get(Guid id)
    {
        return await _dbContext.Orders
            .Include(o => o.ServiceCharge)
            .Include(o => o.Employee)
            .Include(o => o.Reservation)
            .Include(o => o.Customer)
            .Include(o => o.OrderItems)
                .ThenInclude(o => o.Item)
            .Include(o => o.OrderItems)
                .ThenInclude(o => o.ItemVariations)
            .FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<IEnumerable<Order>> GetAll()
    {
        return await _dbContext.Orders.ToListAsync();
    }

    public Task Update(Order entity)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Order>> GetAllFiltered(OrderQueryParameters parameters)
    {
        var query = _dbContext.Orders.AsQueryable();

        if (parameters.Status.HasValue) {
            query = query.Where(s => s.Status == parameters.Status);
        }

        if (parameters.EmployeeId.HasValue) {
            query = query.Where(s => s.EmployeeId == parameters.EmployeeId.Value);
        }

        query = (parameters.OrderBy, parameters.SortOrder) switch {
            (OrderSortableFields.OrderId, SortOrder.Ascending) => query.OrderBy(s => s.Id),
            (OrderSortableFields.OrderId, SortOrder.Descending) => query.OrderByDescending(s => s.Id),

            (OrderSortableFields.EmployeeId, SortOrder.Ascending) => query.OrderBy(s => s.EmployeeId),
            (OrderSortableFields.EmployeeId, SortOrder.Descending) => query.OrderByDescending(s => s.EmployeeId),

            (OrderSortableFields.Status, SortOrder.Ascending) => query.OrderBy(s => s.Status),
            (OrderSortableFields.Status, SortOrder.Descending) => query.OrderByDescending(s => s.Status),

            (OrderSortableFields.OrderCreated, SortOrder.Ascending) => query.OrderBy(s => s.Created),
            (OrderSortableFields.OrderCreated, SortOrder.Descending) => query.OrderByDescending(s => s.Created),

            (OrderSortableFields.OrderClosed, SortOrder.Ascending) => query.OrderBy(s => s.Closed),
            (OrderSortableFields.OrderClosed, SortOrder.Descending) => query.OrderByDescending(s => s.Closed),

            //(OrderSortableFields.DiscountId, SortOrder.Ascending) => query.OrderBy(s => s.DiscountId),
            //(OrderSortableFields.DiscountId, SortOrder.Descending) => query.OrderByDescending(s => s.DiscountId),

            //(OrderSortableFields.DiscountAmount, SortOrder.Ascending) => query.OrderBy(s => s.DiscountAmount),
            //(OrderSortableFields.DiscountAmount, SortOrder.Descending) => query.OrderByDescending(s => s.DiscountAmount),
            
            (OrderSortableFields.ServiceChargeID, SortOrder.Ascending) => query.OrderBy(s => s.ServiceChargeId),
            (OrderSortableFields.ServiceChargeID, SortOrder.Descending) => query.OrderByDescending(s => s.ServiceChargeId),

            //(OrderSortableFields.ServiceChargeAmount, SortOrder.Ascending) => query.OrderBy(s => s.ServiceChargeAmount),
            //(OrderSortableFields.ServiceChargeAmount, SortOrder.Descending) => query.OrderByDescending(s => s.ServiceChargeAmount),

            (OrderSortableFields.TipAmount, SortOrder.Ascending) => query.OrderBy(s => s.TipAmount),
            (OrderSortableFields.TipAmount, SortOrder.Descending) => query.OrderByDescending(s => s.TipAmount),

            //(OrderSortableFields.FinalAmount, SortOrder.Ascending) => query.OrderBy(s => s.FinalAmount),
            //(OrderSortableFields.FinalAmount, SortOrder.Descending) => query.OrderByDescending(s => s.FinalAmount),

            //(OrderSortableFields.PaidAmount, SortOrder.Ascending) => query.OrderBy(s => s.PaidAmount),
            //(OrderSortableFields.PaidAmount, SortOrder.Descending) => query.OrderByDescending(s => s.PaidAmount),

            _ => query.OrderBy(s => s.Id)
        };

        return await query
            .Skip((parameters.Page - 1) * parameters.PageSize)
            .Take(parameters.PageSize)
            .ToListAsync();
    }
}