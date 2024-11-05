using PoS.WebApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using PoS.WebApi.Application.Repositories;
using PoS.WebApi.Infrastructure.Persistence;
using PoS.WebApi.Application.Services.User;
using System.Linq.Dynamic.Core;

namespace PoS.WebApi.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DatabaseContext _dbContext;

    public UserRepository(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Create(User user)
    {
        await _dbContext.Users.AddAsync(user);
    }

    public async Task<IEnumerable<User>> GetAll()
    {
        return await _dbContext.Users.ToListAsync();
    }

    public async Task<User> Get(Guid id)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<IEnumerable<User>> GetAllUsersByFiltering(QueryParameters parameters)
    {
        var allUsers = _dbContext.Users.AsQueryable();

        //Filtering by search
        if(!string.IsNullOrEmpty(parameters.Search))
        {
            allUsers = allUsers.Where(u =>
                u.Username.Contains(parameters.Search) ||
                u.Name.Contains(parameters.Search) ||
                u.Surname.Contains(parameters.Search) ||
                u.Email.Contains(parameters.Search));
        }

        //Filtering by Role
        if (parameters.Role.HasValue)
        {
            allUsers = allUsers.Where(u => u.Role == parameters.Role.Value);
        }

        //Sorting
        if (!string.IsNullOrEmpty(parameters.OrderBy))
        {
            string orderFlow = parameters.OrderAsc ? "ascending" : "descending";
            allUsers = allUsers.OrderBy($"{parameters.OrderBy} {orderFlow}");
        }

        var pagedUsers = await allUsers
                    .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                    .Take(parameters.PageSize)
                    .ToListAsync();

        return pagedUsers; ;
    }

    public async Task<User> GetByUsername(string username)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
    }
}
