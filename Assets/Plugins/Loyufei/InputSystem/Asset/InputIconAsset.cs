using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Loyufei
{
    [CreateAssetMenu(fileName = "InputIconAsset", menuName = "Loyufei/InputSystem/InputIconAsset", order = 1)]
    public class InputIconAsset : EntityFormAsset<Sprite, InputIcon>, IInputIcons
    {
        public void Reset()
        {
            _Entities = new List<InputIcon>();
            
            foreach(EInputKey input in Enum.GetValues(typeof(EInputKey))) 
            {
                _Entities.Add(new(input));
            }
        }

        public Sprite Get(object id) 
        {
            return this[id]?.Data;
        }

        public Sprite Get(int index)
        {
            return index >= _Entities.Count ? default : _Entities[index]?.Data;
        }

        public IEnumerable<Sprite> GetAll() 
        {
            return _Entities.Select(e => e.Data);
        }
    }

    public interface IInputIcons : IGetter<Sprite> 
    {

    }

    [Serializable]
    public class InputIcon : IEntity<Sprite>
    {
        public InputIcon() : this(EInputKey.None, default)
        {

        }

        public InputIcon(EInputKey input) : this(input, default)
        {

        }

        public InputIcon(EInputKey input, Sprite icon)
        {
            _Input = input;
            _Icon  = icon;
        }

        [SerializeField]
        private EInputKey _Input;
        [SerializeField]
        private Sprite _Icon;

        public object Identity => _Input;
        public Sprite Data     => _Icon;
    }
}