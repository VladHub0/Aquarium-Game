using System;

namespace AquariumGame.Features.FishBehaviour.Scripts.Core.Interface
{
    public interface IFishStateMachine : IDisposable
    {
        IFishState Current { get; }

        void Enter<TState>() where TState : class, IFishState;

        void Tick(float deltaTime);
    }
}
