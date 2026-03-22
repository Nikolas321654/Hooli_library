namespace HomeLib.Core.Application.Common.Interfaces;

public interface ICommand<TResponse> { }
public interface ICommand : ICommand<Unit> { }

public record Unit
{
    private static readonly Unit _value = new Unit();
    public static Unit Value => _value;
}

public interface ICommandHandler<in TCommand, TResponse> where TCommand : ICommand<TResponse>
{
    Task<TResponse> Handle(TCommand command, CancellationToken cancellationToken);
}

public interface IQuery<TResponse> { }

public interface IQueryHandler<in TQuery, TResponse> where TQuery : IQuery<TResponse>
{
    Task<TResponse> Handle(TQuery query, CancellationToken cancellationToken);
}
