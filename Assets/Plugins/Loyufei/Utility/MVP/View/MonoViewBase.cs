using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Loyufei.MVP
{
    public class MonoViewBase : MonoBehaviour, IView
    {
        public MonoViewBase() : base () 
        {
            CreateLayoutHandler();
        }

        [SerializeField]
        private bool          _InitActive;
        [SerializeField, Range(0f, 1f)]
        protected float       _FadeDuration = 0.5f;

        protected LayoutHandler _LayoutHandler;

        public virtual object ViewId { get; }

        #region Unity Behaviour

        protected virtual void Awake()
        {
            gameObject.SetActive(_InitActive);
        }

        #endregion

        #region Public Methods

        public virtual IEnumerator Open()
        {
            return ChangeState(true);
        }

        public virtual IEnumerator Close()
        {
            return ChangeState(false);
        }

        public virtual ILayout Layout() 
        {
            _LayoutHandler.Setup(GetListenerAdapter().ToArray());

            return _LayoutHandler;
        }

        public virtual void RemoveLayout() 
        {
            
        }

        #endregion

        #region Protected Methods

        protected virtual void CreateLayoutHandler() 
        {
            _LayoutHandler = new();
        }

        protected virtual IEnumerable<IListenerAdapter> GetListenerAdapter ()
        {
            foreach(IListenerAdapter listener in GetComponentsInChildren<IListenerAdapter>()) 
            {
                yield return listener;
            }
        }

        #endregion

        public virtual IEnumerator ChangeState(bool isOn)
        {
            if (isOn) gameObject.SetActive(true);

            var wait = _FadeDuration;

            for(; wait >= 0f;) 
            {
                yield return wait;

                wait -= Time.fixedDeltaTime;
            }

            if (!isOn) { gameObject.SetActive(false); }
        }

        protected class LayoutHandler : ILayout
        {
            public List<IListenerAdapter> Listeners { get; } = new();

            public void Setup(IEnumerable<IListenerAdapter> listeners) 
            {
                Listeners.Clear();

                Listeners.AddRange(listeners);
            }

            public void BindListenerAll<TListener>(Action<IListenerAdapter, object> callBack) 
                where TListener : IListenerAdapter 
            {
                foreach (var listener in Listeners)
                {
                    if (listener is TListener target)
                    {
                        target.AddListener(callBack);
                    }
                }
            }

            public void BindListener<TListener>(int id, Action<IListenerAdapter, object> callBack) where TListener : IListenerAdapter
            {
                foreach (var listener in Listeners)
                {
                    if (listener is TListener target && target.Id == id)
                    {
                        target.AddListener(callBack);
                    }
                }
            }

            public IEnumerable<T> FindAll<T>()
            {
                foreach (var listener in Listeners) 
                {
                    if (listener is T target) 
                    {
                        yield return target;
                    }
                }
            }

            public T Find<T>(Func<T, bool> match)
            {
                foreach (var listener in Listeners)
                {
                    if (listener is T target && match.Invoke(target))
                    {
                        return target;
                    }
                }

                return default;
            }
        }
    }
}