using System.Text.Json;
using HomeLib.Core;
using HomeLib.Core.Interfaces;
using HomeLib.Core.Interfaces.ForRepositories;
using HomeLib.Core.Model;
using HomeLib.Infrastructure;
using HomeLib.Core.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace HomeLib.Services.Services;

public class UserService(IUsersRepository usersRepository, JwtService jwtService) : IUserService
{
    public async Task<List<User>> GetAllUsers()
    {
        return await usersRepository.GetAllUsersAsync();
    }

    public async Task<User?> GetUserById(Guid id)
    {
        if (id == Guid.Empty) throw new BadRequestException("Invalid Id");
        var user = await usersRepository.GetUserByIdAsync(id);
        return user ?? throw new NotFoundException($"User with {id} id not found");
    }

    public async Task<Guid> RegisterUser(string login, string password, string name)
    {
        var existingUser = await usersRepository.GetUserByLoginAsync(login);
        if (existingUser != null) throw new AlreadyAddedException($"User already with {login} login exists");

        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = name,
            Login = login,
            CreatedAt = DateTime.UtcNow
        };

        var hashPassword = new PasswordHasher<User>().HashPassword(user, password);
        user.Password = hashPassword;
        await usersRepository.AddUserAsync(user);

        return user.Id;
    }

    public async Task<string> Login(string login, string password)
    {
        var account = await usersRepository.GetUserByLoginAsync(login);
        if (account == null) throw new BadRequestException($"User with {login} login not found");

        var result = new PasswordHasher<User>().VerifyHashedPassword(account, account.Password, password);
        return result == PasswordVerificationResult.Failed
            ? throw new BadRequestException("Invalid login or password")
            : jwtService.GenerateJwtToken(account);
    }

    public async Task DeleteUserAsync(Guid id)
    {
        if (id == Guid.Empty) throw new BadRequestException("Invalid id");
        var user = await usersRepository.GetUserByIdAsync(id);
        if (user == null) throw new NotFoundException($"User with {id} id not found");
        await usersRepository.DeleteUserAsync(id);
    }

    public async Task UpdateUserPassword(Guid id, string newPassword, string oldPassword)
    {
        if (id == Guid.Empty) throw new BadRequestException("Invalid id");
        var user = await usersRepository.GetUserByIdAsync(id);
        if (user == null) throw new NotFoundException($"User with {id} not found");

        var result = new PasswordHasher<User>().VerifyHashedPassword(user, user.Password, oldPassword);
        if (result == PasswordVerificationResult.Failed)
            throw new IncorrectOldPasswordException("Incorrect old password");

        var hashPassword = new PasswordHasher<User>().HashPassword(user, newPassword);
        user.Password = hashPassword;
        user.UpdatedAt = DateTime.UtcNow;

        await usersRepository.SaveChangesAsync();
    }
}