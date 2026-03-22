using HomeLib.Core.Application.Common.Interfaces;

namespace HomeLib.Core.Application.Artists.Commands;

public record AddArtistCommand(string Name, bool Grammy) : ICommand<Guid>;

public record UpdateArtistCommand(Guid Id, string Name, bool Grammy) : ICommand;

public record SoftDeleteArtistCommand(Guid Id) : ICommand;

public record HardDeleteArtistCommand(Guid Id) : ICommand;
