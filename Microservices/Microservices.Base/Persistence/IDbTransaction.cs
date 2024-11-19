namespace Microservices.Base.Persistence;

public enum IsolationLevel
{
    ReadUncommitted,
    ReadCommitted,
    RepeatableRead,
    Serializable,
    Snapshot
}

public interface IDbTransaction : IDisposable, IAsyncDisposable
{
    ValueTask CommitAsync();
    ValueTask RollbackAsync();
}
