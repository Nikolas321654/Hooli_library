using HomeLib.Core.Interfaces.ForRepositories;
using HomeLib.Core.Model;
using Microsoft.EntityFrameworkCore;

namespace HomeLib.Infrastructure.Repositories;

public class AlbumRepository(HomeLibDbContext context) : IAlbumRepository
{
    public async Task<List<Album>> GetAllAlbumsAsync(int page, int pageSize,
        CancellationToken cancellationToken = default)
    {
        return await context.Albums.AsNoTracking()
            .Include(t => t.Artist)
            .Include(t => t.Tracks)
            .Where(a => a.IsDeleted == false)
            .OrderBy(a => a.Name)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Album>> GetAllDeletedAlbumsAsync(int page, int pageSize,
        CancellationToken cancellationToken = default)
    {
        return await context.Albums.AsNoTracking()
            .Include(t => t.Artist)
            .Include(t => t.Tracks)
            .Where(a => a.IsDeleted == true)
            .OrderBy(a => a.Name)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<Album?> GetAlbumByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Albums
            .Include(a => a.Artist)
            .Include(t => t.Tracks)
            .FirstOrDefaultAsync(a => a.IsDeleted == false && a.Id == id, cancellationToken);
    }

    public async Task AddAlbumAsync(Album album, CancellationToken cancellationToken = default)
    {
        await context.Albums.AddAsync(album, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task HardDeleteAlbumAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var album = await context.Albums.FindAsync([id], cancellationToken);
        if (album != null)
        {
            context.Albums.Remove(album);
            await context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task UpdateAlbumAsync(Album album, CancellationToken cancellationToken = default)
    {
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<Album>> GetNewAlbums(int count, CancellationToken cancellationToken = default)
    {
        return await context.Albums.AsNoTracking()
            .Include(ta => ta.Artist)
            .Where(t => t.IsDeleted == false)
            .OrderByDescending(t => t.CreatedAt)
            .Take(count)
            .ToListAsync(cancellationToken);
    }
}