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
    private async Task<User?> CheackUserExist(Guid id, CancellationToken cancellationToken = default)
    {
        if (id == Guid.Empty) throw new BadRequestException("Invalid id");
        var user = await usersRepository.GetUserByIdAsync(id, cancellationToken);
        return user ?? throw new NotFoundException($"Invalid login or password");
    }

    public async Task<List<User>> GetAllUsers(CancellationToken cancellationToken = default)
    {
        return await usersRepository.GetAllUsersAsync(cancellationToken);
    }

    public async Task<User?> GetUserById(Guid id, CancellationToken cancellationToken = default)
    {
        if (id == Guid.Empty) throw new BadRequestException("Invalid Id");
        var user = await usersRepository.GetUserByIdAsync(id, cancellationToken);

        return user ?? throw new NotFoundException($"User with {id} id not found");
    }

    public async Task<Guid> RegisterUser(string login, string password, string name,
        CancellationToken cancellationToken = default)
    {
        var existingUser = await usersRepository.GetUserByLoginAsync(login, cancellationToken);
        if (existingUser != null) throw new AlreadyAddedException($"User already with {login} login exists");

        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = name,
            Login = login,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var hashPassword = new PasswordHasher<User>().HashPassword(user, password);
        user.Password = hashPassword;
        await usersRepository.AddUserAsync(user, cancellationToken);

        return user.Id;
    }

    public async Task<string> Login(string login, string password, CancellationToken cancellationToken = default)
    {
        var account = await usersRepository.GetUserByLoginAsync(login, cancellationToken);
        if (account == null) throw new BadRequestException("Invalid login or password");
        var result = new PasswordHasher<User>().VerifyHashedPassword(account, account.Password, password);

        return result == PasswordVerificationResult.Failed
            ? throw new BadRequestException("Invalid login or password")
            : jwtService.GenerateJwtToken(account);
    }

    public async Task DeleteUserAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await CheackUserExist(id, cancellationToken);
        await usersRepository.DeleteUserAsync(id, cancellationToken);
    }

    public async Task UpdateUserPassword(Guid id, string newPassword, string oldPassword,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(newPassword) || string.IsNullOrWhiteSpace(oldPassword))
            throw new BadRequestException("Invalid password");

        switch (newPassword.Length)
        {
            case < 6:
                throw new BadRequestException("Password must be at least 6 characters long");
            case > 12:
                throw new BadRequestException("Password must be at most 12 characters long");
        }

        switch (oldPassword.Length)
        {
            case < 6:
                throw new BadRequestException("Password must be at least 6 characters long");
            case > 12:
                throw new BadRequestException("Password must be at most 12 characters long");
        }

        if (id == Guid.Empty) throw new BadRequestException("Invalid id");

        var user = await CheackUserExist(id, cancellationToken);
        var result = new PasswordHasher<User>().VerifyHashedPassword(user, user.Password, oldPassword);
        if (result == PasswordVerificationResult.Failed)
            throw new IncorrectOldPasswordException("Incorrect old password");

        var hashPassword = new PasswordHasher<User>().HashPassword(user, newPassword);
        user.Password = hashPassword;
        user.UpdatedAt = DateTime.UtcNow;

        await usersRepository.UpdateUserAsync(user, cancellationToken);
    }
}