using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Loyufei
{
    [Serializable]
    public class InputEntity : IEntity<InputEntity>
    {
        public InputEntity()
        {
            _AxisName = string.Empty;
            _Positive = EInputKey.None;
            _Negative = EInputKey.None;
        }

        public InputEntity(InputEntity inputEntity) 
        {
            _AxisName = inputEntity._AxisName;
            _Positive = inputEntity._Positive;
            _Negative = inputEntity._Negative;
        }

        [SerializeField]
        private string    _AxisName;
        [SerializeField]
        private EInputKey _Positive;
        [SerializeField]
        private EInputKey _Negative;

        public EInputKey Positive { get => _Positive; set => _Positive = value; }
        public EInputKey Negative { get => _Negative; set => _Negative = value; }

        public object      Identity => _AxisName;
        public InputEntity Data     => this;

        public void Reset(InputEntity inputEntity) 
        {
            Positive = inputEntity._Positive;
            Negative = inputEntity._Negative;
        }

        public InputEntity Copy() => new InputEntity(this);

        public static InputEntity Empty { get; } = new InputEntity();
    }
}