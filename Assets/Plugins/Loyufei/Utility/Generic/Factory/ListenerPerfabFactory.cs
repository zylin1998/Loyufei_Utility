using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Loyufei.MVP;

namespace Loyufei
{
    public class ListenerPerfabFactory : IFactory<object, Transform, IListenerAdapter>
    {
        public class ListenerPool : MemoryPool<Transform, IListenerAdapter> 
        {
            public static Transform DespawnRoot { get; } = new GameObject(typeof(IListenerAdapter).Name).transform;

            public Action<IListenerAdapter, object> Binder { get; set; }

            protected override void Reinitialize(Transform parent, IListenerAdapter listener)
            {
                if (listener is Component component)
                {
                    component.transform.SetParent(parent);
                    component.transform.localScale = Vector3.one;

                    component.gameObject.SetActive(true);
                }
            }

            protected override void OnDespawned(IListenerAdapter listener)
            {
                if (listener is Component component)
                {
                    component.gameObject.SetActive(false);

                    component.transform.SetParent(DespawnRoot);
                }
            }

            protected override void OnCreated(IListenerAdapter listener)
            {
                listener.AddListener(Binder);
            }
        }

        public ListenerPerfabFactory(IPerfabCollection<IListenerAdapter> collection)
        {
            Collection = collection;
        }

        [Inject]
        DiContainer Container { get; }

        public IPerfabCollection<IListenerAdapter> Collection { get; }

        public Dictionary<object, ListenerPool> Pools { get; } = new();

        public IListenerAdapter Create(object identity, Transform parent)
        {
            return GetPool(identity).Spawn(parent);
        }

        public TListener Create<TListener>(Transform parent) where TListener : IListenerAdapter
        {
            return Create(typeof(TListener), parent).To<TListener>();
        }

        public bool Recycle<TListener>(TListener listener) where TListener : IListenerAdapter 
        {
            var exist = Pools.TryGetValue(typeof(TListener), out var pool);

            if (exist) { pool.Despawn(listener); }

            return exist;
        }

        public void SetBinder<TListener>(Action<IListenerAdapter, object> binder) where TListener : IListenerAdapter
        {
            GetPool(typeof(TListener)).Binder = binder;
        }

        private ListenerPool GetPool(object identity) 
        {
            return Pools.GetorAdd(identity, () =>
            {
                var perfab = Collection.Get(identity);

                Container
                    .BindMemoryPool<IListenerAdapter, ListenerPool>()
                    .WithId(identity)
                    .FromComponentInNewPrefab(perfab);

                return Container.ResolveId<ListenerPool>(identity);
            });
        }
    }
}
