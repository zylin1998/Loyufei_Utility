using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Loyufei.MVP
{
    public interface IView 
    {
        public object ViewId { get; }

        public IEnumerator Open();

        public IEnumerator Close();
    }

    public abstract class ViewMono : MonoBehaviour, IView, IEnumerable<IListenerAdapter>
    {
        [SerializeField]
        private bool _InitActive;

        protected virtual void Awake() 
        {
            gameObject.SetActive(_InitActive);
        }

        public abstract object ViewId { get; }

        public virtual IEnumerator Open() 
        {
            yield return null;
        }

        public IEnumerator Close()
        {
            yield return null;
        }

        public abstract IEnumerator<IListenerAdapter> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public abstract class View : IView
    {
        public View()
        {

        }

        public abstract object ViewId { get; }

        public virtual IEnumerator Open()
        {
            yield return null;
        }

        public IEnumerator Close()
        {
            yield return null;
        }
    }
}