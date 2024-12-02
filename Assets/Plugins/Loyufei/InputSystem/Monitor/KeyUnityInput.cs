using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using Zenject;

namespace Loyufei
{
    public class KeyUnityInput : BaseInput
    {
        [Inject]
        public InputMonitor Monitor { get; }

        public override float GetAxisRaw(string axisName)
        {
            return Monitor.Get(0).GetAxisRaw(axisName);
        }

        public override bool GetButtonDown(string buttonName)
        {
            return Monitor.Get(0).GetButtonDown(buttonName);
        }
    }
}
