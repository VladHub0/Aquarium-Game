using AquariumGame.Features.Fish.Scripts.Services.Registry.Interface;
using AquariumGame.Features.Gameplay.Scripts.Services.Interface;
using AquariumGame.Features.RoundTimer.Scripts.Configs.Interface;
using AquariumGame.Features.RoundTimer.Scripts.Model.Interface;
using AquariumGame.Features.StateMachine.Scripts.GameFlow.Interface;
using UnityEngine;
using Zenject;

namespace AquariumGame.Features.RoundTimer.Scripts.Services
{
    public sealed class RoundTimerService : ITickable
    {
        private readonly IRoundTimerModel _model;
        private readonly IRoundTimerConfig _config;
        private readonly IGameplaySession _session;
        private readonly IFishRegistry _registry;
        private readonly IGameFlowService _gameFlow;

        private bool _wasActive;

        public RoundTimerService(
            IRoundTimerModel model,
            IRoundTimerConfig config,
            IGameplaySession session,
            IFishRegistry registry,
            IGameFlowService gameFlow)
        {
            _model = model;
            _config = config;
            _session = session;
            _registry = registry;
            _gameFlow = gameFlow;
        }

        public void Tick()
        {
            var active = _session.IsActive;
            if (active != _wasActive)
                _model.Reset(_config.Duration);

            _wasActive = active;

            if (!active)
                return;

            _model.Tick(Time.deltaTime);

            if (_model.IsFinished && HasAliveFish())
                _gameFlow.RequestVictory();
        }

        private bool HasAliveFish()
        {
            foreach (var fish in _registry.All)
                if (fish.IsAlive)
                    return true;

            return false;
        }
    }
}
