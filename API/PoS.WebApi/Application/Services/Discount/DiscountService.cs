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
    private readonly IItemRepository _itemRepository;
    private readonly IItemGroupRepository _itemGroupRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DiscountService(
        IDiscountRepository discountRepository, 
        IItemRepository itemRepository,
        IItemGroupRepository itemGroupRepository,
        IUnitOfWork unitOfWork)
    {
        _discountRepository = discountRepository;
        _itemRepository = itemRepository;
        _itemGroupRepository = itemGroupRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task CreateDiscount(CreateDiscountRequest request)
    {
        var discount = new Discount
        {
            BusinessId = request.BusinessId,
            AmountAvailable = request.AmountAvailable,
            IsPercentage = request.IsPercentage,
            Name = request.Name,
            Value = request.Value,
            ValidFrom = request.ValidFrom,
            ValidTo = request.ValidTo
        };
        
        await _discountRepository.Create(discount);
        
        // del sito viso max nesu sure ar gerai veiks :P
        var items = await _itemRepository.GetAll();
        var applicableItems = items
            .Where(i => i.BusinessId == request.BusinessId && request.ApplicableItems.Contains(i.Id));

        foreach (var item in applicableItems)
        {
            item.Discounts.Add(discount);
            discount.Items.Add(item);
        }

        var itemGroups = await _itemGroupRepository.GetAll();
        var applicableGroups = itemGroups
            .Where(i => i.BusinessId == request.BusinessId && request.ApplicableGroups.Contains(i.Id));

        foreach (var itemGroup in applicableGroups)
        {
            itemGroup.Discounts.Add(discount);
            discount.ItemGroups.Add(itemGroup);
        }

        await _unitOfWork.SaveChanges();
    }

    public async Task DeleteDiscountById(DeleteDiscountRequest request)
    {
        var discount = await _discountRepository.Get(request.Id);

        if (discount == null || discount.BusinessId != request.BusinessId)
        {
            return;
        }
        
        await _discountRepository.Delete(request.Id);
        await _unitOfWork.SaveChanges();
    }

    public async Task<GetAllDiscountsResponse> GetAllDiscounts(GetAllDiscountsRequest request)
    {
        var discounts = await _discountRepository.GetAll(request.QueryParameters);
        var discountDtos = discounts
            .Where(d => d.BusinessId == request.BusinessId)
            .Select(d => new DiscountDto
            {
                Name = d.Name,
                Value = d.Value,
                IsPercentage = d.IsPercentage,
                AmountAvailable = d.AmountAvailable,
                ValidFrom = d.ValidFrom,
                ValidTo = d.ValidTo,
                Items = d.Items
                    .Select(i => new ItemDto
                    {
                        Id = i.Id,
                        Name = i.Name
                    }).ToList(),
                ItemGroups = d.ItemGroups
                    .Select(i => new ItemGroupDto
                    {
                        Id = i.Id,
                        Name = i.Name
                    }).ToList()
            });

        return new GetAllDiscountsResponse
        {
            Discounts = discountDtos
        };
    }

    public async Task<GetDiscountResponse> GetDiscount(GetDiscountRequest request)
    {
        var discount = await _discountRepository.Get(request.Id);

        if (discount == null || discount.BusinessId != request.BusinessId)
        {
            return null;
        }
        
        return new GetDiscountResponse
        {
            Discount = new DiscountDto
            {
                Name = discount.Name,
                Value = discount.Value,
                IsPercentage = discount.IsPercentage,
                AmountAvailable = discount.AmountAvailable,
                ValidFrom = discount.ValidFrom,
                ValidTo = discount.ValidTo,
                Items = discount.Items
                    .Select(i => new ItemDto
                    {
                        Id = i.Id,
                        Name = i.Name
                    }).ToList(),
                ItemGroups = discount.ItemGroups
                    .Select(i => new ItemGroupDto
                    {
                        Id = i.Id,
                        Name = i.Name
                    }).ToList()
            }
        };
    }
}

