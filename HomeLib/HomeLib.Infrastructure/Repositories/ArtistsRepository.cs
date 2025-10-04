using HomeLib.Core;
using HomeLib.Core.Interfaces;
using HomeLib.Core.Interfaces.ForRepositories;
using Microsoft.EntityFrameworkCore;

namespace HomeLib.Infrastructure.Repositories;

public class ArtistsRepository(HomeLibDbContext context) : IArtistRepository
{
    public async Task<List<Artist>> GetAllArtistAsync()
    {
        return await context.Artists.AsNoTracking().ToListAsync();
    }

    public async Task AddArtistAsync(Artist artist)
    {
        await context.Artists.AddAsync(artist);
        await context.SaveChangesAsync();
    }
}