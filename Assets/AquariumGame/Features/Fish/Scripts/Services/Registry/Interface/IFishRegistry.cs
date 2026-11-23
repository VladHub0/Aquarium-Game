using System.Collections.Generic;
using AquariumGame.Features.Fish.Scripts.Services.Facade.Interface;

namespace AquariumGame.Features.Fish.Scripts.Services.Registry.Interface
{
    public interface IFishRegistry
    {
        int Count { get; }

        IReadOnlyList<IFishFacade> All { get; }

        void Add(IFishFacade fish);

        void Remove(IFishFacade fish);
    }
}
