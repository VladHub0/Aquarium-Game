using System;
using AquariumGame.Features.StateMachine.Scripts.Core.Interface;

namespace AquariumGame.Features.StateMachine.Scripts.Factory.Interface
{
    public interface IStateFactory
    {
        IState Create(Type stateType);
    }
}
