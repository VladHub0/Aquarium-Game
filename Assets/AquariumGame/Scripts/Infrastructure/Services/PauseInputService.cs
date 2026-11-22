using UnityEngine.InputSystem;
using Zenject;

namespace AquariumGame.Infrastructure.Services
{
    public sealed class PauseInputService : ITickable
    {
        private readonly IGameFlowService _gameFlow;

        public PauseInputService(IGameFlowService gameFlow) => _gameFlow = gameFlow;

        public void Tick()
        {
            var keyboard = Keyboard.current;
            if (keyboard != null && keyboard.escapeKey.wasPressedThisFrame)
                _gameFlow.RequestTogglePause();
        }
    }
}
