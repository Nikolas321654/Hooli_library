using HomeLib.Core.Model;

namespace HomeLib.Core.Interfaces;

public interface IUserService
{
    public Task<List<User>> GetAllUsers();
    public Task<User?> GetUsersById(Guid id);
    public Task<User?> GetUserByLogin(string login);
    public Task RegisterUser(string login, string password, string name);
    public Task<string> Login(string login, string password);
    public Task DeleteUserAsync(Guid id);
}