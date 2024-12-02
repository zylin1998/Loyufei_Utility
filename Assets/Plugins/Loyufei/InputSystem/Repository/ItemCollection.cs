using System;
using System.Collections.Generic;
using System.Linq;

namespace Loyufei
{
    [Serializable]
    public class InputCollection : RepositoryBase<string, InputStorage>, IInputCollection
    {
        public IInputStorage Get(object id)
        {
            return SearchAt(id)?.Data;
        }

        public IInputStorage Get(int index)
        {
            return SearchAt(index)?.Data;
        }

        public IEnumerable<IInputStorage> GetAll()
        {
            return _Reposits.Select(r => r.Data);
        }

        public void Reset(object id, IEnumerable<InputEntity> entities)
        {
            var storage = Get(id);

            if (storage.IsDefault()) 
            {
                _Reposits.Add(new(id.ToString(), new(entities))); 
            }

            Get(id)?.Reset(entities);
        }

        public IEnumerable<InputEntity> Copy(object id)
        {
            foreach (var entity in Get(id)?.GetAll())
            {
                yield return entity.Copy();
            }
        }
    }
}
