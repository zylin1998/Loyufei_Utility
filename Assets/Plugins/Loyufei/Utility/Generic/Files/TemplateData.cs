using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Loyufei
{
    [Serializable]
    public class TemplateData<T> : IAdjustableSaveable<T>
    {
        [SerializeField]
        private List<Structure> _Datas = new();

        public T GetOrAdd(object id, Func<T> add)
        {
            var structure = _Datas.Find(s => s.Hash.Equals(id.GetHashCode()));

            if (structure == null) 
            {
                structure = new Structure(id, add.Invoke());

                _Datas.Add(structure);
            }

            return structure.Data;
        }

        [Serializable]
        protected class Structure 
        {
            public Structure(object id, T data) 
            {
                _Hash = id.GetHashCode();

                _Data = data;
            }

            [SerializeField]
            private int _Hash;
            [SerializeField]
            private T   _Data;

            public int Hash => _Hash;
            public T   Data => _Data;
        } 
    }
}