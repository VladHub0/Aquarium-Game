using System;
using AquariumGame.Features.StateMachine.Scripts.Core.Interface;
using AquariumGame.Features.StateMachine.Scripts.Factory.Interface;
using Zenject;

namespace AquariumGame.Features.StateMachine.Scripts.Factory
{
    public sealed class StateFactory : IStateFactory
    {
        private readonly IInstantiator _instantiator;

        public StateFactory(IInstantiator instantiator) => _instantiator = instantiator;

        public IState Create(Type stateType) => (IState)_instantiator.Instantiate(stateType);
    }
}
