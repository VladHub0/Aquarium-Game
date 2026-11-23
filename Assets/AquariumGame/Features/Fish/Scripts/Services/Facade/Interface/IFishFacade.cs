using System;
using UnityEngine;

namespace AquariumGame.Features.Fish.Scripts.Services.Facade.Interface
{
    public interface IFishFacade : IDisposable
    {
        bool IsAlive { get; }

        void Init();

        void Tick(float deltaTime);

        bool ContainsPoint(Vector2 worldPoint);

        void Tap();
    }
}
