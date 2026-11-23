using AquariumGame.Features.FishBehaviour.Scripts.Core.Interface;

namespace AquariumGame.Features.FishBehaviour.Scripts.Factory.Interface
{
    public interface IFishStateFactory
    {
        TState Get<TState>() where TState : class, IFishState;
    }
}
