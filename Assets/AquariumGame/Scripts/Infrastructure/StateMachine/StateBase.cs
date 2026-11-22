using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AquariumGame.Infrastructure.StateMachine
{
    public abstract class StateBase : IState, IDisposable
    {
        private readonly List<IDisposable> _disposables = new();

        protected void AddDisposable(IDisposable disposable) => _disposables.Add(disposable);
        protected void AddDisposable(Action onDispose) => _disposables.Add(new ActionDisposable(onDispose));

        public virtual UniTask Enter(CancellationToken token) => UniTask.CompletedTask;
        public abstract UniTask<StateTransition> Execute(CancellationToken token);
        public virtual UniTask Exit(CancellationToken token) => UniTask.CompletedTask;

        public void Dispose()
        {
            for (var i = _disposables.Count - 1; i >= 0; i--)
            {
                try { _disposables[i].Dispose(); }
                catch (Exception e) { Debug.LogException(e); }
            }
            _disposables.Clear();
        }

        private sealed class ActionDisposable : IDisposable
        {
            private Action _action;
            public ActionDisposable(Action action) => _action = action;
            public void Dispose()
            {
                _action?.Invoke();
                _action = null;
            }
        }
    }

    public abstract class StateBase<TPayload> : StateBase, IPayloadState<TPayload>
    {
        protected TPayload Payload { get; private set; }
        public void SetPayload(TPayload payload) => Payload = payload;
    }
}
