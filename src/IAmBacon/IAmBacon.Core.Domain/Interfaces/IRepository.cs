namespace IAmBacon.Core.Domain.Interfaces
{
    public interface IRepository<in T> where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
