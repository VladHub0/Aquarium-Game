using System.Threading;
using AquariumGame.Features.StateMachine.Scripts.GameFlow.Interface;
using Cysharp.Threading.Tasks;

namespace AquariumGame.Features.StateMachine.Scripts.GameFlow
{
    public sealed class GameFlowService : IGameFlowService
    {
        private UniTaskCompletionSource _startGame = new();
        private UniTaskCompletionSource _mainMenu = new();
        private UniTaskCompletionSource _togglePause = new();
        private UniTaskCompletionSource _gameOver = new();
        private UniTaskCompletionSource _victory = new();

        public void RequestStartGame() => _startGame.TrySetResult();
        public void RequestMainMenu() => _mainMenu.TrySetResult();
        public void RequestTogglePause() => _togglePause.TrySetResult();
        public void RequestGameOver() => _gameOver.TrySetResult();
        public void RequestVictory() => _victory.TrySetResult();

        public async UniTask WaitForStartGameAsync(CancellationToken token)
        {
            _startGame = new UniTaskCompletionSource();
            await _startGame.Task.AttachExternalCancellation(token);
        }

        public async UniTask WaitForMainMenuAsync(CancellationToken token)
        {
            _mainMenu = new UniTaskCompletionSource();
            await _mainMenu.Task.AttachExternalCancellation(token);
        }

        public async UniTask WaitForTogglePauseAsync(CancellationToken token)
        {
            _togglePause = new UniTaskCompletionSource();
            await _togglePause.Task.AttachExternalCancellation(token);
        }

        public async UniTask WaitForGameOverAsync(CancellationToken token)
        {
            _gameOver = new UniTaskCompletionSource();
            await _gameOver.Task.AttachExternalCancellation(token);
        }

        public async UniTask WaitForVictoryAsync(CancellationToken token)
        {
            _victory = new UniTaskCompletionSource();
            await _victory.Task.AttachExternalCancellation(token);
        }
    }
}
