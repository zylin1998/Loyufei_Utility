using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

namespace Loyufei
{
    public interface IInput
    {
        public IInputUnit this[object id] { get; }

        public object CurrentStorageId { get; set; }
        public int    Index            { get; set; }

        public void Reset(object storageId);

        public void Reset(IInputCollection collection);

        public static IInput Default { get; }

        private struct DefaultInput : IInput
        {
            public IInputUnit this[object id] => IInputUnit.Default;

            public object CurrentStorageId { get; set; }
            public int    Index            { get; set; }

            public void Reset(object storageId)
            {
                CurrentStorageId = storageId;
            }

            public void Reset(IInputCollection collection)
            {
                
            }
        }
    }

    public class InputBase : IInput
    {
        public IInputCollection Collection       { get; private set; }
        public object           CurrentStorageId { get; set; }
        public int              Index            { get; set; }

        protected virtual void Construct(IInputCollection collection) 
        {
            Reset(collection);
        }

        public IInputUnit this[object id] => Get(id);

        protected Dictionary<object, IInputUnit> Units { get; } = new();

        protected IInputUnit Get(object id) 
        {
            var result = Units.GetorAdd(id, () 
                => new InputUnit(Collection.Get(CurrentStorageId)?.Get(id), Index));
            
            return result ?? IInputUnit.Default;
        }

        public void Reset(object storageId)
        {
            CurrentStorageId = storageId;

            Units.Clear();
        }

        public void Reset(IInputCollection collection) 
        {
            Collection = collection;

            Units.Clear();
        }
    }
}