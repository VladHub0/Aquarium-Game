using System.Threading;
using Cysharp.Threading.Tasks;

namespace AquariumGame.Infrastructure.Services
{
    public interface IGameFlowService
    {
        void RequestStartGame();
        void RequestMainMenu();
        void RequestTogglePause();
        UniTask WaitForStartGameAsync(CancellationToken token);
        UniTask WaitForMainMenuAsync(CancellationToken token);
        UniTask WaitForTogglePauseAsync(CancellationToken token);
    }

    public sealed class GameFlowService : IGameFlowService
    {
        private UniTaskCompletionSource _startGame = new();
        private UniTaskCompletionSource _mainMenu = new();
        private UniTaskCompletionSource _togglePause = new();

        public void RequestStartGame() => _startGame.TrySetResult();
        public void RequestMainMenu() => _mainMenu.TrySetResult();
        public void RequestTogglePause() => _togglePause.TrySetResult();

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
    }
}
