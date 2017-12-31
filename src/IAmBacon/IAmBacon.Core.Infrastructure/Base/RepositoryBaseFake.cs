using System.Collections.Generic;
using System.Linq;

namespace IAmBacon.Core.Infrastructure.Base
{
    public class RepositoryBaseFake<T>
    {
        private readonly HashSet<T> _data;

        protected IQueryable<T> Data
        {
            get
            {
                return _data.AsQueryable();
            }
        }

        public RepositoryBaseFake()
        {
            _data = new HashSet<T>();
        }
    }
}
