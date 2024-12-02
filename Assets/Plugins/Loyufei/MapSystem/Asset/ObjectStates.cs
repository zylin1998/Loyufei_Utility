using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Loyufei.MapManagement
{
    public class ObjectStates : RepositoryBase<int, int>, IObjectStates
    {
        public ObjectStates(object category)
        {
            Category = category;
        }

        public object Category { get; }

        public int Get(object id)
        {
            var reposit = SearchAt(id);

            return reposit.IsDefault() ? 0 : reposit.Data;
        }

        public int Get(int index)
        {
            var reposit = SearchAt(index);

            return reposit.IsDefault() ? 0 : reposit.Data;
        }

        public IEnumerable<int> GetAll()
        {
            return _Reposits.Select(r => r.Data);
        }

        public bool Preserve(object id, Func<int, int> preserve)
        {
            var reposit = SearchAt(id);

            if (reposit.IsDefault() || preserve.IsDefault()) { return false; }

            reposit.Preserve(preserve.Invoke(reposit.Data));

            return true;
        }
    }
}