using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loyufei
{
    public interface IGetter<T>
    {
        public T Get(object id);

        public T Get(int index);

        public IEnumerable<T> GetAll();
    }
}
