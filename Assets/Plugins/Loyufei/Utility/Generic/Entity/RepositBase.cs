﻿using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Loyufei
{
    [Serializable]
    public class RepositBase<TId, TData> : Entity<TId, TData>, IReposit<TData>
    {
        #region Constructor

        public RepositBase() : this(default, default) 
        {

        }

        public RepositBase(TId identity, TData data) : base(identity, data)
        {
            
        }

        #endregion

        public virtual void Preserve(TData data)
        {
            _Data = data;
        }

        public virtual void Set(object identify)
        {
            _Identity = identify.To<TId>();
            _Data     = default;
        }

        public virtual void Set(object identify, TData data)
        {
            _Identity = identify.To<TId>();
            _Data     = data;
        }

        public virtual void Release() 
        {
            _Identity = default;
            _Data     = default;
        }

        public virtual bool IsReleased => Identity.Equals(default(TId));
    }
}
