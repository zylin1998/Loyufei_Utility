using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Loyufei.StateMachine
{
    [CreateAssetMenu(fileName = "StateMachine", menuName = "Loyufei/StateMachine/StateMachine Asset")]
    public class StateMachineAsset : StateMachineAssetBase
    {
        [SerializeField]
        protected List<StateAssetBase> _States;

        public override IEnumerable<IState> States => _States;

        public void Add(StateAssetBase state) 
        {
            _States.Add(state);
        }

        public override void Add(IState state)
        {
            if (state is StateAssetBase asset)
            {
                _States.Add(asset);
            }
        }

        public override void Tick()
        {
            Current.Tick();
        }

        public override void FixedTick()
        {
            Current.FixedTick();
        }

        public override void LateTick()
        {
            Current.LateTick();
        }

        public override void SetState(IState state)
        {
            Current.OnExit();

            Current = state ?? IState.Default;

            Current.OnEnter();
        }

        public override void SetState(object identity)
        {
            var state = States.FirstOrDefault(s => Equals(s.Identity, identity));

            if (state.IsDefault()) { return; }

            SetState(state);
        }

        public override void Transfer()
        {
            if (!Current.CanExit) { return; }

            var next = _States.Find(state => state.CanEnter) ?? IState.Default;

            SetState(next);
        }
    }
}
