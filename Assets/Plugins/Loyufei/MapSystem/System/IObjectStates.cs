using System;
using System.Collections;
using System.Collections.Generic;

namespace Loyufei.MapManagement
{
    public interface IObjectStates : IGetter<int>
    {
        public object Category { get; }

        public bool Preserve(object id, Func<int, int> preserve);
    }
}