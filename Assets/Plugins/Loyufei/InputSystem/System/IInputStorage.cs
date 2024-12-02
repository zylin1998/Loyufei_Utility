using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Loyufei
{
    public interface IInputStorage : IGetter<InputEntity>
    {
        public void Reset(IEnumerable<InputEntity> entities);
    }
}
