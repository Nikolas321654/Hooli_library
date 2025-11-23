using HomeLib.Core;
using HomeLib.Core.Interfaces;
using HomeLib.Core.Interfaces.ForRepositories;
using HomeLib.Core.Model;
using Microsoft.EntityFrameworkCore;

namespace HomeLib.Infrastructure.Repositories;

public class UsersRepository(HomeLibDbContext context) : IUsersRepository
{
    public async Task<List<User>> GetAllUsersAsync(CancellationToken cancellationToken = default)
    {
        return await context.Users.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<User?> GetUserByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<User?> GetUserByLoginAsync(string login, CancellationToken cancellationToken = default)
    {
        return await context.Users.FirstOrDefaultAsync(x => x.Login == login, cancellationToken);
    }

    public async Task AddUserAsync(User user, CancellationToken cancellationToken = default)
    {
        context.Users.Add(user);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteUserAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var user = await context.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (user != null)
        {
            context.Users.Remove(user);
            await context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task UpdateUserAsync(User user, CancellationToken cancellationToken = default)
    {
        context.Users.Update(user);
        await context.SaveChangesAsync(cancellationToken);
    }
}