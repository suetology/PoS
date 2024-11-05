namespace PoS.WebApi.Application.Services.NewFolder;

using Domain.Entities;
using PoS.WebApi.Application.Repositories;
using PoS.WebApi.Application.Services.User;
using PoS.WebApi.Application.Services.User.Contracts;
using PoS.WebApi.Domain.Common;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UserService(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task CreateUser(UserDto userDto)
    {
        var user = userDto.ToDomain();

        await _userRepository.Create(user);
        await _unitOfWork.SaveChanges();
    }

    public async Task<IEnumerable<User>> GetAllUsers(QueryParameters parameters)
    {
        return await _userRepository.GetAllUsersByFiltering(parameters);
    }

    public async Task<User> GetUser(Guid userId)
    {
        return await _userRepository.Get(userId);
    }
}
