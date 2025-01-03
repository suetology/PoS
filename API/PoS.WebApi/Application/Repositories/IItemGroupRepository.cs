﻿using PoS.WebApi.Application.Services.ItemGroup;
using PoS.WebApi.Domain.Common;
using PoS.WebApi.Domain.Entities;

namespace PoS.WebApi.Application.Repositories;

public interface IItemGroupRepository : IRepository<ItemGroup>
{
    Task<IEnumerable<ItemGroup>> GetAllGroupsByFiltering(QueryParameters parameters);
    Task<bool> ExistsAsync(Guid groupId);
}