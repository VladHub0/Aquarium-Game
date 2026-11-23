using System;

namespace AquariumGame.Features.Fish.Scripts.Services.Facade.Interface
{
    public interface IFishFacade : IDisposable
    {
        void Init();

        void Tick(float deltaTime);
    }
}
