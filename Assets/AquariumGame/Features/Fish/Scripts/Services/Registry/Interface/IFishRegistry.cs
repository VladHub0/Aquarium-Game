using AquariumGame.Features.Fish.Scripts.Services.Facade.Interface;

namespace AquariumGame.Features.Fish.Scripts.Services.Registry.Interface
{
    public interface IFishRegistry
    {
        int Count { get; }

        void Add(IFishFacade fish);

        void Remove(IFishFacade fish);
    }
}
