using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Loyufei.MVP
{
    public enum EShowViewMode
    {
        Single   = 0,
        Additive = 1,
    }

    public class ViewManager
    {
        public ViewManager() 
        {
            Views = new();
        }

        public Dictionary<object, IView> Views { get; }

        public IView Current { get; private set; }

        private Stack<IView> Actives { get; } = new();

        public void Register(IView view)
        {
            Views.Add(view.ViewId, view);
        }

        public bool Unregister(object key)
        {
            return Views.Remove(key);
        }

        public IObservable<long> Show(object id, EShowViewMode showMode = EShowViewMode.Single, bool reset = false)
        {
            if (!Views.TryGetValue(id, out var view)) { return default; }

            Actives.Push(view);

            Current = Actives.Peek();
            
            return view.Open();
        }

        public IObservable<long> Close() 
        {
            if (Current.IsDefault()) { return default; }

            var close = Actives.Pop();

            if(Actives.TryPeek(out var view))
            {
                Current = view; 
            }
            
            return close.Close();
        }
    }

    public class ViewStateObservable : IObservable<long>, IObserver<long> 
    {
        public ViewStateObservable(IView open, IView close) 
        {
            Open  = open;
            Close = close;

            Close.Close().Subscribe(OnNext, OnError, () =>
            {
                Open.Open().Subscribe(OnNext, OnError, OnCompleted);
            });
        }

        public IView Open  { get; }
        public IView Close { get; }

        public Subject<long> Subject { get; }

        public void OnNext(long next) => Subject.OnNext(next);

        public void OnCompleted() => Subject.OnCompleted();

        public void OnError(Exception error) => Subject.OnError(error);

        public IDisposable Subscribe(IObserver<long> observer) => Subject.Subscribe(observer);
    }
}