using HomeLib.Core.Model;

namespace HomeLib.Core.Interfaces.ForRepositories;

public interface IUsersRepository
{
    public Task<List<User>> GetAllUsersAsync();
    public Task<User?> GetUsersByLoginAsync(string login);
    public Task<User?> GetUsersByIdAsync(Guid id);
    public Task AddUserAsync(User user);
    public Task DeleteUserAsync(Guid id);
}