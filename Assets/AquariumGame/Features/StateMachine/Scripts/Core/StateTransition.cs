using System;
using AquariumGame.Features.StateMachine.Scripts.Core.Interface;

namespace AquariumGame.Features.StateMachine.Scripts.Core
{
    public readonly struct StateTransition
    {
        public readonly Type StateType;
        internal readonly Action<IState> ApplyPayload;

        public bool IsExit => StateType == null;

        internal StateTransition(Type stateType, Action<IState> applyPayload)
        {
            StateType = stateType;
            ApplyPayload = applyPayload;
        }
    }

    public static class Transition
    {
        public static StateTransition GoTo<TState>() where TState : class, IState =>
            new StateTransition(typeof(TState), null);

        public static StateTransition GoTo<TState, TPayload>(TPayload payload)
            where TState : class, IPayloadState<TPayload> =>
            new StateTransition(typeof(TState), state => ((TState)state).SetPayload(payload));

        public static StateTransition Exit() => default;
    }
}
