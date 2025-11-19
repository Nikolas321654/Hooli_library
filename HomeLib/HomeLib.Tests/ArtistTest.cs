using HomeLib.Core.Model;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace HomeLib.Tests;

public class ArtistTest : IntegrationTestBase
{
    [Fact]
    public async Task CreateArtist_ShouldCreateNewArtist()
    {
        var id = Guid.NewGuid();
        var artistToInsert = new Artist
        {
            Id = id,
            Name = "TestArtist",
            Grammy = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        Context.Artists.Add(artistToInsert);
        await Context.SaveChangesAsync();

        var savedArtist = await Context.Artists.FindAsync(artistToInsert.Id);
        Assert.NotNull(savedArtist);
        Assert.Equal("TestArtist", savedArtist.Name);
        Assert.Equal(true, savedArtist.Grammy);
        Assert.Equal(id, savedArtist.Id);
    }

    [Fact]
    public async Task GetAllArtists_ShouldReturnAllArtists()
    {
        var id1 = Guid.NewGuid();
        var id2 = Guid.NewGuid();
        var artist1 = new Artist
        {
            Id = id1,
            Name = "TestArtist1",
            Grammy = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var artist2 = new Artist
        {
            Id = id2,
            Name = "TestArtist2",
            Grammy = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        Context.Artists.Add(artist1);
        Context.Artists.Add(artist2);
        await Context.SaveChangesAsync();

        var savedArtists = await Context.Artists.ToListAsync();

        Assert.Equal(2, savedArtists.Count);
        Assert.Contains(savedArtists, a => a.Id == id1);
        Assert.Contains(savedArtists, a => a.Id == id2);
    }

    [Fact]
    public async Task UpdateArtist_ShouldUpdateArtist()
    {
        var id = Guid.NewGuid();
        var artistToUpdate = new Artist
        {
            Id = id,
            Name = "TestArtist",
            Grammy = false,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        await Context.Artists.AddAsync(artistToUpdate);
        await Context.SaveChangesAsync();
        var savedArtist = await Context.Artists.FindAsync(artistToUpdate.Id);

        savedArtist.Name = "TestArtist2";
        savedArtist.Grammy = true;
        savedArtist.UpdatedAt = DateTime.UtcNow;

        await Context.SaveChangesAsync();
        var savedArtist2 = await Context.Artists.FindAsync(savedArtist.Id);

        Assert.NotNull(savedArtist2);
        Assert.Equal("TestArtist2", savedArtist2.Name);
        Assert.Equal(true, savedArtist2.Grammy);
        Assert.Equal(id, savedArtist2.Id);
    }

    [Fact]
    public async Task DeleteArtist_ShouldDeleteArtist()
    {
        var id = Guid.NewGuid();
        var artistToDelete = new Artist
        {
            Id = id,
            Name = "TestArtist",
            Grammy = false,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        await Context.Artists.AddAsync(artistToDelete);
        await Context.SaveChangesAsync();

        var savedArtist = await Context.Artists.FindAsync(artistToDelete.Id);
        Assert.NotNull(savedArtist);
        Context.Artists.Remove(savedArtist);
        await Context.SaveChangesAsync();

        var savedArtist2 = await Context.Artists.FirstOrDefaultAsync(x => x.Id == id);
        Assert.Null(savedArtist2);
    }

    [Fact]
    public async Task FindArtist_ShouldReturnArtist()
    {
        var id = Guid.NewGuid();
        var artist = new Artist
        {
            Id = id,
            Name = "TestArtist",
            Grammy = false,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        await Context.Artists.AddAsync(artist);
        await Context.SaveChangesAsync();

        var savedArtist = await Context.Artists.FirstOrDefaultAsync(x => x.Name == artist.Name);
        Assert.NotNull(savedArtist);
        Assert.Equal("TestArtist", savedArtist.Name);
        Assert.Equal(false, savedArtist.Grammy);
        Assert.Equal(id, savedArtist.Id);
    }
}