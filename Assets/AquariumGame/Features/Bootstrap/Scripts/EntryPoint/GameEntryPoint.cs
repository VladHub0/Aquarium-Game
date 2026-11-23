using System;
using System.Threading;
using AquariumGame.Features.Bootstrap.Scripts.States;
using AquariumGame.Features.StateMachine.Scripts.Core;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace AquariumGame.Features.Bootstrap.Scripts.EntryPoint
{
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
