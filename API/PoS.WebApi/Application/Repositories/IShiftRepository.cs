using PoS.WebApi.Domain.Common;
using PoS.WebApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PoS.WebApi.Application.Repositories;
public interface IShiftRepository : IRepository<Shift>
{
    Task<IEnumerable<Shift>> GetShiftsByFilters(Guid? employeeId = null, DateTime? fromDate = null, DateTime? toDate = null);
    Task Delete(Guid id);
}