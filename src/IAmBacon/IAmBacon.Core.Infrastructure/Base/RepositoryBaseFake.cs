using System.Collections.Generic;

namespace IAmBacon.Core.Infrastructure.Base
{
    public class RepositoryBaseFake<T>
    {
        protected HashSet<T> Data { get; }

        public RepositoryBaseFake()
        {
            Data = new HashSet<T>();
        }
    }
}
