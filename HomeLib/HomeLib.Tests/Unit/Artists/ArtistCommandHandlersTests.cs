using HomeLib.Core.Application.Artists.Commands;
using HomeLib.Core.Application.Artists.Handlers;
using HomeLib.Core.Interfaces.ForRepositories;
using HomeLib.Core.Model;
using Moq;
using Xunit;

namespace HomeLib.Tests.Unit.Artists;

public class ArtistCommandHandlersTests
{
    private readonly Mock<IArtistRepository> _artistRepositoryMock;

    public ArtistCommandHandlersTests()
    {
        _artistRepositoryMock = new Mock<IArtistRepository>();
    }

    [Fact]
    public async Task AddArtistCommandHandler_ShouldAddArtist()
    {
        var handler = new AddArtistCommandHandler(_artistRepositoryMock.Object);
        var command = new AddArtistCommand("Test Artist", true);


        var result = await handler.Handle(command, CancellationToken.None);

        Assert.NotEqual(Guid.Empty, result);
        _artistRepositoryMock.Verify(r => r.AddArtistAsync(It.Is<Artist>(a =>
            a.Name == command.Name &&
            a.Grammy == command.Grammy), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task UpdateArtistCommandHandler_ShouldUpdateArtist()
    {
        var artistId = Guid.NewGuid();
        var existingArtist = new Artist { Id = artistId, Name = "Old Name", Grammy = false };
        _artistRepositoryMock.Setup(r => r.GetArtistByIdAsync(artistId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingArtist);

        var handler = new UpdateArtistCommandHandler(_artistRepositoryMock.Object);
        var command = new UpdateArtistCommand(artistId, "New Name", true);

        await handler.Handle(command, CancellationToken.None);

        Assert.Equal("New Name", existingArtist.Name);
        Assert.True(existingArtist.Grammy);
        _artistRepositoryMock.Verify(r => r.UpdateArtistAsync(existingArtist, It.IsAny<CancellationToken>()),
            Times.Once);
    }
}