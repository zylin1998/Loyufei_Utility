using System;
using System.Linq;
using System.Collections.Generic;

namespace Loyufei.MVP
{
    public interface ILayout
    {
        public void BindListenerAll<TListener>(Action<IListenerAdapter, object> callBack) where TListener : IListenerAdapter;

        public void BindListener<TListener>(int id, Action<IListenerAdapter, object> callBack) where TListener : IListenerAdapter;

        public IEnumerable<T> FindAll<T>();

        public T Find<T>(Func<T, bool> match);
    }
}
