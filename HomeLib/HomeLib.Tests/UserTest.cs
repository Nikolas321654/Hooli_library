using HomeLib.Core.Model;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace HomeLib.Tests;

public class UserTest : IntegrationTestBase
{
    [Fact]
    public async Task CreateUser_ShouldCreateUser()
    {
        var id = Guid.NewGuid();
        var user = new User
        {
            Id = id,
            Name = "User",
            Password = "password",
            Login = "login",
            Version = 1,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        
        Context.Users.Add(user);
        await Context.SaveChangesAsync();

        var savedUser = await Context.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
        Assert.NotNull(savedUser);
        Assert.Equal("User", savedUser.Name);
        Assert.Equal("password", savedUser.Password);
        Assert.Equal("login", savedUser.Login);
        Assert.Equal(1, savedUser.Version);
    }

    [Fact]
    public async Task UpdateUser_ShouldUpdateUser()
    {
        var id = Guid.NewGuid();
        var user = new User
        {
            Id = id,
            Name = "User",
            Password = "password",
            Login = "login",
            Version = 1,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        Context.Users.Add(user);
        await Context.SaveChangesAsync();

        var savedUser = await Context.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
        savedUser.Name = "UpdatedUser";
        savedUser.Password = "UpdatedPassword";
        savedUser.Login = "UpdatedLogin";
        savedUser.Version = 2;
        savedUser.UpdatedAt = DateTime.UtcNow;
        await Context.SaveChangesAsync();

        var updatedUser = await Context.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
        Assert.Equal("UpdatedUser", updatedUser.Name);
        Assert.Equal("UpdatedPassword", updatedUser.Password);
        Assert.Equal("UpdatedLogin", updatedUser.Login);
        Assert.Equal(2, updatedUser.Version);
    }

    [Fact]
    public async Task DeleteUser_ShouldDeleteUser()
    {
        var id = Guid.NewGuid();
        var user = new User
        {
            Id = id,
            Name = "User",
            Password = "password",
            Login = "login",
            Version = 1,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        Context.Users.Add(user);
        await Context.SaveChangesAsync();

        var savedUser = await Context.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
        Context.Users.Remove(savedUser);
        await Context.SaveChangesAsync();
        
        var deletedUser = await Context.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
        Assert.Null(deletedUser);
    }

    [Fact]
    public async Task FindUser_ShouldReturnUser()
    {
        var id = Guid.NewGuid();
        var user = new User
        {
            Id = id,
            Name = "User",
            Password = "password",
            Login = "login",
            Version = 1,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        Context.Users.Add(user);
        await Context.SaveChangesAsync();
        
        var savedUser = await Context.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
        Assert.NotNull(savedUser);
        Assert.Equal("User", savedUser.Name);
        Assert.Equal("password", savedUser.Password);
        Assert.Equal("login", savedUser.Login);
        Assert.Equal(1, savedUser.Version);
    }
}