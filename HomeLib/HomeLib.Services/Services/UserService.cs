using HomeLib.Core;
using HomeLib.Core.Interfaces;
using HomeLib.Core.Interfaces.ForRepositories;
using HomeLib.Core.Model;
using HomeLib.Infrastructure;
using HomeLib.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;

namespace HomeLib.Services.Services;

public class UserService(IUsersRepository usersRepository, JwtService jwtService) : IUserService
{
    public async Task<List<User>> GetAllUsers()
    {
        return await usersRepository.GetAllUsersAsync();
    }

    public async Task<User?> GetUsersById(Guid id)
    {
        return await usersRepository.GetUsersByIdAsync(id);
    }

    public async Task RegisterUser(string login, string password, string name)
    {
        var user = new User
        {
            Name = name,
            Login = login,
            CreatedAt = DateTime.UtcNow
        };

        var hashPassword = new PasswordHasher<User>().HashPassword(user, password);
        user.Password = hashPassword;
        await usersRepository.AddUserAsync(user);
    }

    public async Task<User?> GetUserByLogin(string login)
    {
        return (await usersRepository.GetUsersByLoginAsync(login));
    }

    public async Task<string> Login(string login, string password)
    {
        var account = await usersRepository.GetUsersByLoginAsync(login);
        var result = new PasswordHasher<User>().VerifyHashedPassword(account, account.Password, password);
        return result == PasswordVerificationResult.Failed
            ? throw new ApplicationException("Invalid login or password")
            : jwtService.GenerateJwtToken(account);
    }

    public async Task DeleteUserAsync(Guid id)
    {
        await usersRepository.DeleteUserAsync(id);
    }
}