﻿using PoS.WebApi.Domain.Common;
using PoS.WebApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PoS.WebApi.Application.Repositories;
public interface IShiftRepository : IRepository<Shift>
{
    Task<IEnumerable<Shift>> GetShiftsByFilters(Guid? employeeId, DateTime? fromDate, DateTime? toDate);
    Task Delete(Guid id);
}