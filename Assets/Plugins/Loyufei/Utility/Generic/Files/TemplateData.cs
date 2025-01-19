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
        private List<Structure<T>> _Datas = new();

        public T GetOrAdd(object id, Func<T> add)
        {
            var structure = _Datas.Find(s => s.Hash.Equals(id.GetHashCode()));

            if (structure == null) 
            {
                structure = new Structure<T>(id, add.Invoke());

                _Datas.Add(structure);
            }

            return structure.Data;
        }
    }

    [Serializable]
    public class Structure<T>
    {
        public Structure(object id, T data)
        {
            _Hash = id.GetHashCode();

            _Data = data;
        }

        [SerializeField]
        private int _Hash;
        [SerializeField]
        private T _Data;

        public int Hash => _Hash;
        public T Data => _Data;
    }
}