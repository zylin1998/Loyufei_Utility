using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Loyufei 
{
    [CreateAssetMenu(fileName = "DefaultInput", menuName = "Loyufei/InputSystem/DefaultInput", order = 1)]
    public class DefaultInputAsset : EntityFormAsset<InputStorage, Entity<string, InputStorage>>, IInputCollection
    {
        public IEnumerable<InputEntity> Copy(object id)
        {
            foreach (var entity in Get(id)?.GetAll()) 
            {
                yield return entity.Copy();
            }
        }

        public IInputStorage Get(object id)
        {
            return this[id]?.Data;
        }

        public IInputStorage Get(int index)
        {
            return index >= _Entities.Count ? default : _Entities[index].Data;
        }

        public IEnumerable<IInputStorage> GetAll()
        {
            return _Entities.Select(e => e.Data);
        }

        void IInputCollection.Reset(object id, IEnumerable<InputEntity> entities)
        {
            Get(id)?.Reset(entities);
        }
    }
}
