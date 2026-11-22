using System;
using System.Threading;
using AquariumGame.Infrastructure.StateMachine;
using AquariumGame.Infrastructure.States;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace AquariumGame.Bootstrap
{
    /// <summary>
    /// Точка входа игры. Zenject вызывает Initialize() после сборки контейнера
    /// в Bootstrap-сцене, и мы запускаем глобальную state machine.
    /// </summary>
    public sealed class GameEntryPoint : IInitializable, IDisposable
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly CancellationTokenSource _cts = new();

        public GameEntryPoint(IGameStateMachine stateMachine) => _stateMachine = stateMachine;

        public void Initialize() => RunAsync(_cts.Token).Forget();

        private async UniTaskVoid RunAsync(CancellationToken token)
        {
            try
            {
                await _stateMachine.Run<BootstrapState>(token);
            }
            catch (OperationCanceledException)
            {
                // Нормальное завершение при выходе из игры
            }
            catch (Exception e)
            {
                Debug.LogError("[GameEntryPoint] Глобальная state machine упала с ошибкой.");
                Debug.LogException(e);
            }
        }

        public void Dispose()
        {
            _cts.Cancel();
            _cts.Dispose();
        }
    }
}
