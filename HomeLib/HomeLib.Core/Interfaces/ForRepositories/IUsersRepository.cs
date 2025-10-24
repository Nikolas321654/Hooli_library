using HomeLib.Core.Model;

namespace HomeLib.Core.Interfaces.ForRepositories;

public interface IUsersRepository
{
    public Task<List<User>> GetAllUsersAsync();
    public Task<User?> GetUserByLoginAsync(string login);
    public Task<User?> GetUserByIdAsync(Guid id);
    public Task AddUserAsync(User user);
    public Task DeleteUserAsync(Guid id);
    public Task SaveChangesAsync();
}