using System;
using AquariumGame.Features.Hud.Scripts.View.Interface;
using AquariumGame.Features.StateMachine.Scripts.GameFlow.Interface;
using Zenject;

namespace AquariumGame.Features.Hud.Scripts.Presenter
{
    public sealed class HudBarPresenter : IInitializable, IDisposable
    {
        private readonly IHudBarView _view;
        private readonly IGameFlowService _gameFlow;

        public HudBarPresenter(IHudBarView view, IGameFlowService gameFlow)
        {
            _view = view;
            _gameFlow = gameFlow;
        }

        public void Initialize() => _view.MenuClicked += OnMenuClicked;

        public void Dispose() => _view.MenuClicked -= OnMenuClicked;

        private void OnMenuClicked() => _gameFlow.RequestTogglePause();
    }
}
