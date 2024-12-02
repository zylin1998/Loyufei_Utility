using System;
using System.Linq;
using System.Collections.Generic;

namespace Loyufei
{
    public interface IInputCollection : IGetter<IInputStorage>
    {
        public void Reset(object id, IEnumerable<InputEntity> entities);

        public IEnumerable<InputEntity> Copy(object id);
    }
}
