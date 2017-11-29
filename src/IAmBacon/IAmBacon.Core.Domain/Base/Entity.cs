namespace IAmBacon.Core.Domain.Base
{
    public abstract class Entity
    {
        // Using private fields, to allow for encapsulation since this is permitted in EF Core 1.1 or greater
        // https://docs.microsoft.com/en-us/ef/core/modeling/backing-field
        private int _id;

        public int Id
        {
            get { return _id; }
            protected set { _id = value; }
        }
    }
}
