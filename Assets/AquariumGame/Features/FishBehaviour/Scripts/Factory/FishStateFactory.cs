using System;
using System.Collections.Generic;
using AquariumGame.Features.FishBehaviour.Scripts.Core.Interface;
using AquariumGame.Features.FishBehaviour.Scripts.Factory.Interface;
using Zenject;

namespace AquariumGame.Features.FishBehaviour.Scripts.Factory
{
    public sealed class FishStateFactory : IFishStateFactory
    {
        private readonly IInstantiator _instantiator;
        private readonly Dictionary<Type, IFishState> _states = new Dictionary<Type, IFishState>();

        public FishStateFactory(IInstantiator instantiator) => _instantiator = instantiator;

        public TState Get<TState>() where TState : class, IFishState
        {
            if (_states.TryGetValue(typeof(TState), out var state))
                return (TState)state;

            var created = _instantiator.Instantiate<TState>();
            _states[typeof(TState)] = created;
            return created;
        }
    }
}
