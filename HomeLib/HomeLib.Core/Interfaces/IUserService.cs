using HomeLib.Core.Model;

namespace HomeLib.Core.Interfaces;

public interface IUserService
{
    public Task<List<User>> GetAllUsers();
    public Task<User?> GetUserById(Guid id);
    public Task<Guid> RegisterUser(string login, string password, string name);
    public Task<string> Login(string login, string password);
    public Task DeleteUserAsync(Guid id);
    public Task UpdateUserPassword(Guid id, string newPassword, string oldPassword);
}