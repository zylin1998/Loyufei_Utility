using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Loyufei
{
    public interface IReposit : IEntity
    {
        public void Preserve(object data);

        public void Release();

        public void Set(object identity);
        public void Set(object identify, object data);
    }

    public interface IReposit<T> : IReposit, IEntity<T>
    {
        public void Preserve(T data);

        public void Set(object identity, T data);

        void IReposit.Preserve(object data)
        {
            if (data is T t) { Preserve(t); }
        }

        void IReposit.Set(object identity, object data)
        {
            Set(identity, data.To<T>());
        }
    }
}
