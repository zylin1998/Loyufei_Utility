using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Loyufei
{
    public class MonoPool<TMono> : MemoryPool<Transform, TMono> where TMono : Component
    {
        public Transform DespawnRoot { get; } = new GameObject(typeof(TMono).Name).transform;

        protected override void Reinitialize(Transform parent, TMono mono)
        {
            mono.transform.SetParent(parent);
            mono.transform.localScale = Vector3.one;

            mono.gameObject.SetActive(true);
        }

        protected override void OnDespawned(TMono mono)
        {
            mono.gameObject.SetActive(false);

            mono.transform.SetParent(DespawnRoot);
        }
    }
}
