using Microsoft.EntityFrameworkCore;

namespace Microservices.Persistence;

using Base.Persistence;

public abstract class DatabaseContext : DbContext, IDatabaseService
{

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    public override int SaveChanges()
    {
        return base.SaveChanges();
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return base.SaveChangesAsync(cancellationToken);
    }

    public IDbTransaction BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
    {
        System.Data.IsolationLevel current = isolationLevel switch
        {
            IsolationLevel.Serializable => System.Data.IsolationLevel.Serializable,
            IsolationLevel.ReadUncommitted => System.Data.IsolationLevel.ReadUncommitted,
            IsolationLevel.RepeatableRead => System.Data.IsolationLevel.RepeatableRead,
            IsolationLevel.Snapshot => System.Data.IsolationLevel.Snapshot,
            _ => System.Data.IsolationLevel.ReadCommitted
        };
        return new DbTransaction(this, this.Database.BeginTransaction(current));
    }

    /// <summary>
    /// Clear all entities from the context
    /// </summary>
    public void Clean()
    {
        ChangeTracker.Entries()
            .ToList()
            .ForEach(e => e.State = EntityState.Detached);
    }
}

