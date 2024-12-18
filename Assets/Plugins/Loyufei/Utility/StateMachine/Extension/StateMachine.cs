using System;
using System.Linq;
using System.Collections.Generic;

namespace Loyufei.StateMachine
{
    public static class StateMachine
    {
        public static IStateMachine Basic() 
        {
            return new StateMachineBase();
        }

        public static ITickableStateMachine Update()
        {
            return new UpdateStateMachine();
        }

        public static ITickableStateMachine Update(this IStateMachine self) 
        {
            return new UpdateStateMachine(self);
        }

        public static ITickableStateMachine Update(ETickMode tickMode)
        {
            return new TickableStateMachine(tickMode);
        }

        public static ITickableStateMachine Update(this IStateMachine self, ETickMode tickMode)
        {
            return new TickableStateMachine(self, tickMode);
        }

        public static ITickableStateMachine FixedUpdate()
        {
            return new FixedUpdateStateMachine();
        }

        public static ITickableStateMachine FixedUpdate(this IStateMachine self)
        {
            return new FixedUpdateStateMachine(self);
        }

        public static ITickableStateMachine LateUpdate()
        {
            return new LateUpdateStateMachine();
        }

        public static ITickableStateMachine LateUpdate(this IStateMachine self)
        {
            return new LateUpdateStateMachine(self);
        }

        public static ITickableStateMachine Interval(TimeSpan timeSpan)
        {
            return new IntervaStateMachine(timeSpan);
        }

        public static ITickableStateMachine Interval(this IStateMachine self, TimeSpan timeSpan)
        {
            return new IntervaStateMachine(self, timeSpan);
        }

        public static ISequenceStateMachine Sequence() 
        {
            return new SequenceStateMachine();
        }

        public static ISequenceStateMachine Sequence(this IStateMachine self)
        {
            return new SequenceStateMachine(self);
        }

        public static ISequenceStateMachine Sequence(this IStateMachine self, ITransitionOrder order)
        {
            return new SequenceStateMachine(self, order);
        }

        public static IPhaseStateMachine Phase()
        {
            return new PhaseStateMachine();
        }

        public static IPhaseStateMachine Phase(this IStateMachine self)
        {
            return new PhaseStateMachine(self);
        }

        public static IPhaseStateMachine Phase(this ISequenceStateMachine self)
        {
            return new PhaseStateMachine(self)
                .ExitWhen(() => !self.Active);
        }

        public static ISequenceStateMachine OrderBy(this ISequenceStateMachine self, ITransitionOrder order) 
        {
            self.Order = order;

            return self;
        }

        public static ISequenceStateMachine OrderBy(this ISequenceStateMachine self, params object[] orders) 
        {
            return self.OrderBy(new TransitionOrder(orders));
        }

        public static ISequenceStateMachine OrderBy(this ISequenceStateMachine self, IEnumerable<object> orders)
        {
            return self.OrderBy(new TransitionOrder(orders));
        }

        public static TStateMachine Add<TStateMachine>(this TStateMachine self, params IState[] states) where TStateMachine : IStateMachine
        {
            foreach (var state in states) 
            {
                self.Add(state);
            }

            return self;
        }

        public static TStateMachine Add<TStateMachine>(this TStateMachine self, IEnumerable<IState> states) where TStateMachine : IStateMachine
        {
            foreach (var state in states)
            {
                self.Add(state);
            }

            return self;
        }

        public static IFunctionalState CreateState() 
        {
            return new FunctionalState();
        }
    }
}
