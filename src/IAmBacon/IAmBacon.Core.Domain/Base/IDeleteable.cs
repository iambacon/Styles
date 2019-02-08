namespace IAmBacon.Core.Domain.Base
{
    // https://www.ryansouthgate.com/2019/01/07/entity-framework-core-soft-delete/
    public interface IDeleteable
    {
        bool IsDeleted { get; }
    }
}
