using System;
using System.Linq;
using System.Collections.Generic;
using Zenject;
using UnityEngine;

namespace Loyufei
{
    public class PerfabFactory<TAbstract> : IFactory<object, Transform, TAbstract>
    {
        public class Pool : MemoryPool<Transform, TAbstract> 
        {
            public static Transform DespawnRoot { get; } = new GameObject(typeof(TAbstract).Name).transform;

            protected override void Reinitialize(Transform parent, TAbstract perfab)
            {
                if (perfab is Component component) 
                {
                    component.transform.SetParent(parent);

                    component.gameObject.SetActive(true);
                }
            }

            protected override void OnDespawned(TAbstract perfab)
            {
                if (perfab is Component component) 
                {
                    component.transform.SetParent(DespawnRoot);

                    component.gameObject.SetActive(false);
                }
            }
        }

        public PerfabFactory(IPerfabCollection<TAbstract> collection) 
        {
            Collection = collection;
        }

        [Inject]
        DiContainer Container { get; }

        public IPerfabCollection<TAbstract> Collection { get; private set; }

        public Dictionary<object, Pool> Pools { get; } = new();

        public TAbstract Create(object identity, Transform parent)
        {
            return GetPool(identity).Spawn(parent);
        }

        public bool Recycle(TAbstract recycle, object identity) 
        {
            var exist = Pools.TryGetValue(identity, out var pool);

            if (exist) { pool.Despawn(recycle); }

            return exist;
        }

        private Pool GetPool(object identity) 
        {
            return Pools.GetorAdd(identity, () =>
            {
                var perfab = Collection.Get(identity);

                Container
                    .BindMemoryPool<TAbstract, Pool>()
                    .WithId(identity)
                    .FromComponentInNewPrefab(perfab);

                return Container.ResolveId<Pool>(identity);
            });
        }
    }
}
