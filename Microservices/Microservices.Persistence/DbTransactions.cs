using System.Data;

namespace Microservices.Persistence;

using Base.Persistence;
using Microsoft.EntityFrameworkCore.Storage;

sealed class DbTransaction : IDbTransaction
{
    private bool successFully = false;
    public DbTransaction(DatabaseContext databaseContext, IDbContextTransaction currentTransaction)
    {
        _databaseContext = databaseContext;
        _currentTransaction = currentTransaction;
    }

    readonly IDbContextTransaction _currentTransaction;
    readonly DatabaseContext _databaseContext;

    public async ValueTask CommitAsync()
    {
        await _databaseContext.SaveChangesAsync();  //ensure savechanges
        await _currentTransaction.CommitAsync();
        successFully = true;
    }

    public async ValueTask RollbackAsync()
        => await _currentTransaction.RollbackAsync();

    public bool TransactionSuccessull => successFully;

    public void Dispose()
    {
        _currentTransaction.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        Dispose();
        return ValueTask.CompletedTask;
    }
}
