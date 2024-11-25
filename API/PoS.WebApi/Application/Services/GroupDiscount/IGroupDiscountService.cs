namespace PoS.WebApi.Application.Services.GroupDiscount;
using Domain.Entities;
using PoS.WebApi.Application.Services.GroupDiscount.Contracts;

public interface IGroupDiscountService
{
    Task AssignDiscountToGroupAsync(Guid discountId, Guid itemGroupId);
    Task RemoveDiscountFromGroupAsync(Guid discountId, Guid itemGroupId);
    Task<IEnumerable<GroupDiscount>> GetDiscountsByGroupAsync(Guid itemGroupId);
    Task<IEnumerable<GroupDiscount>> GetGroupsByDiscountAsync(Guid discountId);
    Task<IEnumerable<GroupDiscountDto>> GetAllGroups();
}
