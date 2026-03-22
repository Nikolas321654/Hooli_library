using HomeLib.Core.Application.Common.Interfaces;
using HomeLib.Core.Application.Artists.Commands;
using HomeLib.Core.Exceptions;
using HomeLib.Core.Interfaces.ForRepositories;
using HomeLib.Core.Model;

namespace HomeLib.Core.Application.Artists.Handlers;

public class AddArtistCommandHandler(IArtistRepository artistRepository) 
    : ICommandHandler<AddArtistCommand, Guid>
{
    public async Task<Guid> Handle(AddArtistCommand command, CancellationToken cancellationToken)
    {
        var id = Guid.NewGuid();
        var artist = new Artist()
        {
            Id = id,
            Name = command.Name,
            Grammy = command.Grammy,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsDeleted = false
        };

        await artistRepository.AddArtistAsync(artist, cancellationToken);
        return id;
    }
}

public class UpdateArtistCommandHandler(IArtistRepository artistRepository) 
    : ICommandHandler<UpdateArtistCommand, Unit>
{
    public async Task<Unit> Handle(UpdateArtistCommand command, CancellationToken cancellationToken)
    {
        if (command.Id == Guid.Empty) throw new BadRequestException("Id cannot be empty");
        var artist = await artistRepository.GetArtistByIdAsync(command.Id, cancellationToken);
        if (artist == null) throw new NotFoundException($"Artist with {command.Id} id not found");

        artist.Name = command.Name;
        artist.Grammy = command.Grammy;
        artist.UpdatedAt = DateTime.UtcNow;

        await artistRepository.UpdateArtistAsync(artist, cancellationToken);
        return Unit.Value;
    }
}

public class SoftDeleteArtistCommandHandler(IArtistRepository artistRepository) 
    : ICommandHandler<SoftDeleteArtistCommand, Unit>
{
    public async Task<Unit> Handle(SoftDeleteArtistCommand command, CancellationToken cancellationToken)
    {
        if (command.Id == Guid.Empty) throw new BadRequestException("Id cannot be empty");
        var artist = await artistRepository.GetArtistByIdAsync(command.Id, cancellationToken);
        if (artist == null) throw new NotFoundException($"Artist with {command.Id} id not found");

        artist.IsDeleted = true;
        artist.UpdatedAt = DateTime.UtcNow;

        await artistRepository.UpdateArtistAsync(artist, cancellationToken);
        return Unit.Value;
    }
}

public class HardDeleteArtistCommandHandler(IArtistRepository artistRepository) 
    : ICommandHandler<HardDeleteArtistCommand, Unit>
{
    public async Task<Unit> Handle(HardDeleteArtistCommand command, CancellationToken cancellationToken)
    {
        if (command.Id == Guid.Empty) throw new BadRequestException("Id cannot be empty");
        var artist = await artistRepository.GetArtistByIdAsync(command.Id, cancellationToken);
        if (artist == null) throw new NotFoundException($"Artist with {command.Id} id not found");

        await artistRepository.HardDeleteArtistAsync(command.Id, cancellationToken);
        return Unit.Value;
    }
}
