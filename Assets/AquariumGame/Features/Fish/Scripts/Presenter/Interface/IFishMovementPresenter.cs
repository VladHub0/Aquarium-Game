using System;
using UnityEngine;

namespace AquariumGame.Features.Fish.Scripts.Presenter.Interface
{
    public interface IFishMovementPresenter : IDisposable
    {
        void Tick(float deltaTime);

        bool MoveTowards(Vector3 target, float speed, float reachDistance, float deltaTime);

        void Stop();
    }
}
