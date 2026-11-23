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
}
