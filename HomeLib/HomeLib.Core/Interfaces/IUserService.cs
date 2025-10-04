namespace HomeLib.Core.Interfaces;

public interface IUserService
{
    public Task<List<User>> GetAllUsers();
}