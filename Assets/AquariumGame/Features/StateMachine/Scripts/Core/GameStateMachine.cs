using System;
using System.Threading;
using AquariumGame.Features.StateMachine.Scripts.Core.Interface;
using AquariumGame.Features.StateMachine.Scripts.Factory.Interface;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AquariumGame.Features.StateMachine.Scripts.Core
{
    public sealed class GameStateMachine : IGameStateMachine
    {
        private readonly IStateFactory _stateFactory;

        public IState CurrentState { get; private set; }
        public bool IsRunning { get; private set; }
        public event Action<IState> StateChanged;

        public GameStateMachine(IStateFactory stateFactory) => _stateFactory = stateFactory;

        public UniTask Run<TState>(CancellationToken token) where TState : class, IState =>
            RunLoop(Transition.GoTo<TState>(), token);

        public UniTask Run<TState, TPayload>(TPayload payload, CancellationToken token)
            where TState : class, IPayloadState<TPayload> =>
            RunLoop(Transition.GoTo<TState, TPayload>(payload), token);

        private async UniTask RunLoop(StateTransition transition, CancellationToken token)
        {
            if (IsRunning)
                throw new InvalidOperationException($"{nameof(GameStateMachine)} уже запущена.");

            IsRunning = true;
            try
            {
                while (!transition.IsExit)
                {
                    token.ThrowIfCancellationRequested();

                    var state = _stateFactory.Create(transition.StateType);
                    transition.ApplyPayload?.Invoke(state);

                    CurrentState = state;
                    StateChanged?.Invoke(state);
                    Debug.Log($"[GameStateMachine] -> {state.GetType().Name}");

                    try
                    {
                        await state.Enter(token);
                        transition = await state.Execute(token);
                    }
                    finally
                    {
                        await ExitState(state);
                    }
                }
            }
            finally
            {
                CurrentState = null;
                IsRunning = false;
            }
        }

        private static async UniTask ExitState(IState state)
        {
            try
            {
                await state.Exit(CancellationToken.None);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
            finally
            {
                (state as IDisposable)?.Dispose();
            }
        }
    }
}
