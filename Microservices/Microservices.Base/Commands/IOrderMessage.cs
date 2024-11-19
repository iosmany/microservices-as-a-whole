
namespace Microservices.Base.Commands;

public interface IOrder
{
    Guid TransactionId { get; }
    DateTime Timestamp { get; }
}

public interface ISaveOrder<E> : IOrder
{
    E Entity { get; }
}

public interface IDeleteOrder<K> : IOrder
{
    K EntityId { get; }
}

public abstract class SaveOrderBase<E> : ISaveOrder<E>
{
    public E Entity { get; }
    public Guid TransactionId { get; }
    public DateTime Timestamp { get; }
}

public abstract class DeleteOrderBase<K> : IDeleteOrder<K>
{
    public K EntityId { get; }
    public Guid TransactionId { get; }
    public DateTime Timestamp { get; }
}