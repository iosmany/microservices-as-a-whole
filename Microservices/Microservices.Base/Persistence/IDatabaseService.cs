

using System.Data;

namespace Microservices.Base.Persistence;

public interface IDatabaseService
{
    IDbTransaction BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
}
