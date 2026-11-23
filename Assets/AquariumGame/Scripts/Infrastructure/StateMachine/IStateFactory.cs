using System;
using Zenject;

namespace AquariumGame.Infrastructure.StateMachine
{
    public interface IStateFactory
    {
        IState Create(Type stateType);
    }

    public sealed class StateFactory : IStateFactory
    {
        private readonly IInstantiator _instantiator;

        public StateFactory(IInstantiator instantiator) => _instantiator = instantiator;

        public IState Create(Type stateType) => (IState)_instantiator.Instantiate(stateType);
    }
}
