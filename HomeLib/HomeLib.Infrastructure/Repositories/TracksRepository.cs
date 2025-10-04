using HomeLib.Core;
using HomeLib.Core.Interfaces;
using HomeLib.Core.Interfaces.ForRepositories;
using Microsoft.EntityFrameworkCore;

namespace HomeLib.Infrastructure.Repositories;

public class TracksRepository(HomeLibDbContext context) : ITrackRepository
{
    public async Task<List<Track>> GetAllTracksAsync()
    {
        return await context.Tracks.AsNoTracking().ToListAsync();
    }

    public async Task AddTrackAsync(Track track)
    {
        await context.Tracks.AddAsync(track);
        await context.SaveChangesAsync();
    }
}