using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Loyufei.StateMachine
{
    public interface IState : IIdentity
    {
        public bool CanEnter { get; }
        public bool CanExit { get; }

        public void OnEnter();
        public void Tick();
        public void FixedTick();
        public void LateTick();
        public void OnExit();

        public static IState Default { get; } = new DefaultState();
    }
}