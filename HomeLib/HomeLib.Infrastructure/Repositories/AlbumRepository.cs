using HomeLib.Core;
using HomeLib.Core.Interfaces.ForRepositories;
using HomeLib.Core.Model;
using Microsoft.EntityFrameworkCore;

namespace HomeLib.Infrastructure.Repositories;

public class AlbumRepository(HomeLibDbContext context) : IAlbumRepository
{
    public async Task<List<Album>> GetAllAlbumsAsync()
    {
        return await context.Albums.AsNoTracking()
            .Include(t => t.Artist)
            .Include(t => t.Tracks)
            .ToListAsync();
    }

    public Task<Album?> GetAlbumByIdAsync(Guid id)
    {
        return context.Albums
            .Include(a => a.Artist)
            .Include(t => t.Tracks)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public Task AddAlbumAsync(Album album)
    {
        context.Albums.Add(album);
        return context.SaveChangesAsync();
    }

    public async Task DeleteAlbumAsync(Guid id)
    {
        var album = await context.Albums.FindAsync(id);
        if (album != null)
        {
            context.Albums.Remove(album);
            await context.SaveChangesAsync();
        }
    }

    public Task UpdateAlbumAsync(Album album)
    {
        context.Albums.Update(album);
        return context.SaveChangesAsync();
    }
}