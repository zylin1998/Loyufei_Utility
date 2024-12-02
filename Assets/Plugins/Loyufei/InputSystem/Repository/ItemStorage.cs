using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Loyufei
{
    [Serializable]
    public class InputStorage : EntityForm<InputEntity, InputEntity>, IInputStorage
    {
        public InputStorage(IEnumerable<InputEntity> entities) : base(entities)
        {
            _Entities = entities.ToList();
        }

        [SerializeField]
        private List<InputEntity> _Entities;

        public override Dictionary<object, InputEntity> Dictionary
            => _Entities.ToDictionary(k => k.Identity);

        public override IEntity<InputEntity> this[object identity]
            => _Entities.FirstOrDefault(e => Equals(e.Identity, identity));

        public InputEntity Get(object id)
        {
            var entity = this[id];

            return entity.IsDefault() ? InputEntity.Empty : entity.Data;
        }

        public InputEntity Get(int index)
        {
            if (index >= _Entities.Count) { return InputEntity.Empty; }

            return _Entities[index];
        }

        public IEnumerable<InputEntity> GetAll()
        {
            return _Entities;
        }

        public void Reset(IEnumerable<InputEntity> entities)
        {
            _Entities = entities.ToList();
        }
    }
}
