namespace HomeLib.Core.Interfaces.ForRepositories;

public interface IUsersRepository
{
    public Task<List<User>> GetAllUsersAsync();
}