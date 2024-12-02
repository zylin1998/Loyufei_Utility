using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Loyufei
{
    public interface IInputOptions : IGetter<InputOption>
    {

    }

    [CreateAssetMenu(fileName = "InputOptions", menuName = "Loyufei/InputSystem/InputOptions", order = 1)]
    public class InputOptionAsset : EntityFormAsset<InputOption, InputOption>, IInputOptions
    {
        public InputOption Get(object id)
        {
            return this[id].Data;
        }

        public InputOption Get(int index)
        {
            return index < _Entities.Count ? _Entities[index] : default;
        }

        public IEnumerable<InputOption> GetAll()
        {
            return _Entities;
        }
    }

    [Serializable]
    public class InputOption : IEntity<InputOption>
    {
        [SerializeField]
        private string _OptionId;
        [SerializeField]
        private string _TargetId;
        [SerializeField]
        private EPositive _Positive = EPositive.Positive;

        public object Identity => _OptionId;
        public InputOption Data => this;
        public string TargetId => _TargetId;
        public EPositive Positive => _Positive;
    }
}