using HomeLib.Core;
using HomeLib.Core.Interfaces.ForRepositories;
using HomeLib.Core.Model;
using Microsoft.EntityFrameworkCore;

namespace HomeLib.Infrastructure.Repositories;

public class AlbumRepository(HomeLibDbContext context) : IAlbumRepository
{
    public async Task<List<Album>> GetAllAlbumsAsync()
    {
        return await context.Albums
            .Include(t => t.Artist)
            .Include(t => t.Tracks)
            .Where(a => a.IsDeleted == false)
            .ToListAsync();
    }

    public async Task<List<Album>> GetAllDeletedAlbumsAsync()
    {
        return await context.Albums
            .Include(t => t.Artist)
            .Include(t => t.Tracks)
            .Where(a => a.IsDeleted == true)
            .ToListAsync();
    }

    public Task<Album?> GetAlbumByIdAsync(Guid id)
    {
        return context.Albums
            .Include(a => a.Artist)
            .Include(t => t.Tracks)
            .FirstOrDefaultAsync(a => a.IsDeleted == false && a.Id == id);
    }

    public Task AddAlbumAsync(Album album)
    {
        context.Albums.Add(album);
        return context.SaveChangesAsync();
    }

    public async Task HardDeleteAlbumAsync(Guid id)
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
    
    public async Task<List<Album>> GetNewAlbums(int count)
    {
        return await context.Albums
            .Include(ta => ta.Artist)
            .Where(t => t.IsDeleted == false)
            .OrderByDescending(t => t.CreatedAt)
            .Take(count)
            .ToListAsync();
    }
}