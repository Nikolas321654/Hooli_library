using HomeLib.Core.Model;

namespace HomeLib.Core.Interfaces.ForRepositories;

public interface IUsersRepository
{
    public Task<List<User>> GetAllUsersAsync(CancellationToken cancellationToken = default);
    public Task<User?> GetUserByLoginAsync(string login, CancellationToken cancellationToken = default);
    public Task<User?> GetUserByIdAsync(Guid id, CancellationToken cancellationToken = default);
    public Task AddUserAsync(User user, CancellationToken cancellationToken = default);
    public Task DeleteUserAsync(Guid id, CancellationToken cancellationToken = default);
    public Task UpdateUserAsync(User user, CancellationToken cancellationToken = default);
}