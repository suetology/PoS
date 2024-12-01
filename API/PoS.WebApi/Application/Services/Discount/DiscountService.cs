namespace PoS.WebApi.Application.Services.Discount;

using Domain.Entities;
using PoS.WebApi.Application.Repositories;
using PoS.WebApi.Application.Services.Discount.Contracts;
using PoS.WebApi.Domain.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class DiscountService : IDiscountService
{
    private readonly IDiscountRepository _discountRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DiscountService(IDiscountRepository discountRepository, IUnitOfWork unitOfWork)
    {
        _discountRepository = discountRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task CreateDiscount(DiscountDto discountDto)
    {
        var discount = discountDto.ToDomain();

        await _discountRepository.Create(discount);
        await _unitOfWork.SaveChanges();
    }

    public async Task DeleteDiscountById(Guid id)
    {
        await _discountRepository.Delete(id);
        await _unitOfWork.SaveChanges();
    }

    public async Task<IEnumerable<DiscountWithGroupsDto>> GetAllDiscounts(QueryParameters parameters)
    {
        return await _discountRepository.GetAll(parameters);
    }

    public async Task<DiscountWithGroupsDto> GetDiscount(Guid discountId)
    {
        return await _discountRepository.GetDto(discountId);
    }
}

