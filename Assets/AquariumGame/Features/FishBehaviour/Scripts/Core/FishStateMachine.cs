using AquariumGame.Features.FishBehaviour.Scripts.Core.Interface;
using AquariumGame.Features.FishBehaviour.Scripts.Factory.Interface;

namespace AquariumGame.Features.FishBehaviour.Scripts.Core
{
    public sealed class FishStateMachine : IFishStateMachine
    {
        private readonly IFishStateFactory _factory;

        private bool _disposed;

        public FishStateMachine(IFishStateFactory factory) => _factory = factory;

        public IFishState Current { get; private set; }

        public void Enter<TState>() where TState : class, IFishState
        {
            if (_disposed)
                return;

            var next = _factory.Get<TState>();
            if (ReferenceEquals(next, Current))
                return;

            Current?.Exit();
            Current = next;
            Current.Enter();
        }

        public void Tick(float deltaTime)
        {
            if (!_disposed)
                Current?.Tick(deltaTime);
        }

        public void Dispose()
        {
            if (_disposed)
                return;

            _disposed = true;
            Current?.Exit();
            Current = null;
        }
    }
}
