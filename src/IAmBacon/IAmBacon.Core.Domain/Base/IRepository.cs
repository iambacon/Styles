namespace IAmBacon.Core.Domain.Base
{
    public interface IRepository<in T> where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
