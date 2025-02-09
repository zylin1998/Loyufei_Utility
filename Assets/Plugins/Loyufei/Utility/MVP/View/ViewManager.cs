using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using System.Xml.Schema;

namespace Loyufei.MVP
{
    public enum EShowViewMode
    {
        Single   = 0,
        Additive = 1,
    }

    public class ViewManager
    {
        public Dictionary<object, IView> Views { get; } = new();

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

        public IObservable<object> Show(object id)
        {
            var subject = new Subject<object>();
            
            if (!Views.TryGetValue(id, out var view)) { return subject; }

            var coroutine  = view.Open();
            var observable = Observable
                .EveryUpdate()
                .TakeWhile((f) => coroutine.MoveNext())
                .Subscribe((f) => subject.OnNext(coroutine.Current), subject.OnError, subject.OnCompleted);

            return subject;
        }

        public IObservable<object> Close(object id) 
        {
            var subject = new Subject<object>();

            if (!Views.TryGetValue(id, out var view)) { return subject; }

            var coroutine  = view.Close();
            var observable = Observable
                .EveryUpdate()
                .TakeWhile((f) => coroutine.MoveNext())
                .Subscribe((f) => subject.OnNext(coroutine.Current), subject.OnError, subject.OnCompleted);

            return subject;
        }
    }
}