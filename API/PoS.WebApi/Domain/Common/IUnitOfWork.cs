﻿namespace PoS.WebApi.Domain.Common;

public interface IUnitOfWork
{
    Task<int> SaveChanges();
}