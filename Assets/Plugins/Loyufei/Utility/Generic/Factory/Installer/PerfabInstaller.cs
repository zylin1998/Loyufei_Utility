using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Loyufei
{
    public abstract class PerfabInstaller<TAbstract> : PerfabInstallerBase<TAbstract>
    {
        [Serializable]
        protected class Entity : IEntity<GameObject> 
        {
            [SerializeField]
            private string _Id;
            [SerializeField]
            private GameObject _Perfab;

            public object     Identity => _Id;
            public GameObject Data     => _Perfab;
        }

        [SerializeField]
        private List<Entity> _Perfabs;

        public override IEntity<GameObject> this[object identity] 
            => _Perfabs.Find(e => Equals(e.Identity, identity));

        public override GameObject Get(object id) 
        {
            var entity = this[id];

            return HasAbstract(entity?.Data) ? entity.Data : default;
        }

        public override GameObject Get(int index) 
        {
            if (index >= _Perfabs.Count) { return default; }

            var entity = _Perfabs[index];

            return HasAbstract(entity.Data) ? entity.Data : default;
        }

        public override IEnumerable<GameObject> GetAll() 
        {
            foreach (var entity in _Perfabs) 
            {
                if (HasAbstract(entity.Data)) 
                {
                    yield return entity.Data;
                }
            }
        }

        public override IEnumerator<IEntity<GameObject>> GetEnumerator()
        {
            return _Perfabs.GetEnumerator();
        }

        private bool HasAbstract(GameObject gameObject) 
        {
            if (!gameObject) { return false; }

            return !gameObject.GetComponent<TAbstract>().IsDefault();
        }
    }

    public abstract class PerfabInstallerBase<TAbstract> 
        : ScriptableObjectInstaller<PerfabInstallerBase<TAbstract>>, IPerfabCollection<TAbstract>, IEntityForm<GameObject, IEntity<GameObject>>
    {
        [SerializeField]
        protected string _Category;

        public abstract IEntity<GameObject> this[object identity] { get; }

        public abstract GameObject Get(object id);
        public abstract GameObject Get(int index);
        public abstract IEnumerable<GameObject> GetAll();

        public abstract IEnumerator<IEntity<GameObject>> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override void InstallBindings()
        {
            Container
                .Bind<PerfabFactory<TAbstract>>()
                .WithId(_Category)
                .AsCached()
                .WithArguments((IPerfabCollection<TAbstract>)this);
        }
    }
}
