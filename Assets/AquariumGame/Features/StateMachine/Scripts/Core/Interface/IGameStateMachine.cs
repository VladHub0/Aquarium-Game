using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace AquariumGame.Features.StateMachine.Scripts.Core.Interface
{
    public interface IGameStateMachine
    {
        IState CurrentState { get; }
        bool IsRunning { get; }
        event Action<IState> StateChanged;

        UniTask Run<TState>(CancellationToken token) where TState : class, IState;

        UniTask Run<TState, TPayload>(TPayload payload, CancellationToken token)
            where TState : class, IPayloadState<TPayload>;
    }
}
