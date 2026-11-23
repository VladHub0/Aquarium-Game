using System;

namespace AquariumGame.Features.Fish.Scripts.Presenter.Interface
{
    public interface IFishMovementPresenter : IDisposable
    {
        void Tick(float deltaTime);
    }
}
