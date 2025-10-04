using HomeLib.Core;
using HomeLib.Core.Interfaces;
using HomeLib.Core.Interfaces.ForRepositories;
using HomeLib.Infrastructure.Repositories;

namespace HomeLib.Services.Services;

public class UserService(IUsersRepository usersRepository) : IUserService
{
    public async Task<List<User>> GetAllUsers()
    {
        var usersList = await usersRepository.GetAllUsersAsync();
        return usersList;
    }
}