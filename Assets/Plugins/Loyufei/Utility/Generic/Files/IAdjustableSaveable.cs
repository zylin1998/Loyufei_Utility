using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loyufei
{
    public interface IAdjustableSaveable<T> : ISaveable
    {
        public T GetOrAdd(object id, Func<T> add);
    }
}
