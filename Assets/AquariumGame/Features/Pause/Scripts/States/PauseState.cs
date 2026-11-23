using System.Threading;
using AquariumGame.Features.AssetManagement.Scripts.Keys;
using AquariumGame.Features.Gameplay.Scripts.Services.Interface;
using AquariumGame.Features.Gameplay.Scripts.States;
using AquariumGame.Features.MainMenu.Scripts.States;
using AquariumGame.Features.SceneLoading.Scripts.Services.Interface;
using AquariumGame.Features.StateMachine.Scripts.Core;
using AquariumGame.Features.StateMachine.Scripts.GameFlow.Interface;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AquariumGame.Features.Pause.Scripts.States
{
    public sealed class PauseState : StateBase
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly IGameFlowService _gameFlow;
        private readonly IGameplaySession _session;

        private float _timeScale = 1f;
        private bool _resuming;

        public PauseState(ISceneLoader sceneLoader, IGameFlowService gameFlow, IGameplaySession session)
        {
            _sceneLoader = sceneLoader;
            _gameFlow = gameFlow;
            _session = session;
        }

        public override UniTask Enter(CancellationToken token)
        {
            _timeScale = Time.timeScale;
            Time.timeScale = 0f;
            return _sceneLoader.LoadOverlayAsync(AssetKeys.PauseScene, token);
        }

        public override async UniTask<StateTransition> Execute(CancellationToken token)
        {
            using var cts = CancellationTokenSource.CreateLinkedTokenSource(token);

            var index = await UniTask.WhenAny(
                _gameFlow.WaitForTogglePauseAsync(cts.Token),
                _gameFlow.WaitForMainMenuAsync(cts.Token));

            cts.Cancel();

            if (index == 0)
            {
                _resuming = true;
                return Transition.GoTo<GameplayState>();
            }

            return Transition.GoTo<MainMenuState>();
        }

        public override async UniTask Exit(CancellationToken token)
        {
            Time.timeScale = _timeScale;

            if (!_resuming)
                _session.End();

            await _sceneLoader.UnloadOverlayAsync(token);
        }
    }
}
