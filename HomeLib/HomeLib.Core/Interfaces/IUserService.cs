using HomeLib.Core.Model;

namespace HomeLib.Core.Interfaces;

public interface IUserService
{
    public Task<List<User>> GetAllUsers(CancellationToken cancellationToken);
    public Task<User?> GetUserById(Guid id, CancellationToken cancellationToken);
    public Task<Guid> RegisterUser(string login, string password, string name, CancellationToken cancellationToken);
    public Task<string> Login(string login, string password, CancellationToken cancellationToken);
    public Task DeleteUserAsync(Guid id, CancellationToken cancellationToken);

    public Task UpdateUserPassword(Guid id, string newPassword, string oldPassword,
        CancellationToken cancellationToken);
}