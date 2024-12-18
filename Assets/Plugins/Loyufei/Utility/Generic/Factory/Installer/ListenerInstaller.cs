using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Loyufei.MVP;

namespace Loyufei 
{
    [CreateAssetMenu(fileName = "Listener Installer", menuName = "Loyufei/Installer/Listener Installer")]
    public class ListenerInstaller : PerfabInstallerBase<IListenerAdapter>
    {
        protected struct Entity : IEntity<GameObject>
        {
            public Entity(object identity, GameObject perfab)
            {
                Identity = identity;
                Data     = perfab;
            }
            
            public object     Identity { get; }
            public GameObject Data     { get; }
        }

        [SerializeField]
        protected List<GameObject> _Perfabs;

        public override IEntity<GameObject> this[object identity]
            => new Entity(identity, _Perfabs.Find(p => Equals(p.GetComponent<IListenerAdapter>()?.GetType(), identity)));

        public override GameObject Get(object id)
        {
            var entity = this[id];

            return entity.Data;
        }

        public override GameObject Get(int index)
        {
            if (index >= _Perfabs.Count) { return default; }

            var perfab = _Perfabs[index];

            return HasAbstract(perfab) ? perfab : default;
        }

        public override IEnumerable<GameObject> GetAll()
        {
            foreach (var perfab in _Perfabs)
            {
                if (HasAbstract(perfab))
                {
                    yield return perfab;
                }
            }
        }

        public override IEnumerator<IEntity<GameObject>> GetEnumerator()
        {
            foreach (var perfab in GetAll()) 
            {
                yield return new Entity(perfab.GetComponent<IListenerAdapter>().GetType(), perfab);
            }
        }

        public override void InstallBindings()
        {
            Container
                .Bind<ListenerPerfabFactory>()
                .WithId(_Category)
                .AsCached()
                .WithArguments((IPerfabCollection<IListenerAdapter>)this);
        }

        private bool HasAbstract(GameObject gameObject)
        {
            if (!gameObject) { return false; }

            return !gameObject.GetComponent<IListenerAdapter>().IsDefault();
        }
    }
}
