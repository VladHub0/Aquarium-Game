using AquariumGame.Features.Fish.Scripts.Services.Registry.Interface;
using AquariumGame.Features.Gameplay.Scripts.Services.Interface;
using AquariumGame.Features.StateMachine.Scripts.GameFlow.Interface;
using Zenject;

namespace AquariumGame.Features.GameOver.Scripts.Services
{
    public sealed class GameOverWatcher : ITickable
    {
        private readonly IGameplaySession _session;
        private readonly IFishRegistry _registry;
        private readonly IGameFlowService _gameFlow;

        public GameOverWatcher(IGameplaySession session, IFishRegistry registry, IGameFlowService gameFlow)
        {
            _session = session;
            _registry = registry;
            _gameFlow = gameFlow;
        }

        public void Tick()
        {
            if (_session.IsActive && _registry.Count == 0)
                _gameFlow.RequestGameOver();
        }
    }
}
