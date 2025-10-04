using HomeLib.Core;
using HomeLib.Core.Interfaces;
using HomeLib.Core.Interfaces.ForRepositories;
using Microsoft.EntityFrameworkCore;

namespace HomeLib.Infrastructure.Repositories;

public class UsersRepository(HomeLibDbContext context) : IUsersRepository
{
    public async Task<List<User>> GetAllUsersAsync()
    {
        return await context.Users.AsNoTracking().ToListAsync();
    }
}