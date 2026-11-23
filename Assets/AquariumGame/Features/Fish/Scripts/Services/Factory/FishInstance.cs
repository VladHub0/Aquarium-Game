using AquariumGame.Features.Fish.Scripts.Services.Facade.Interface;
using UnityEngine;

namespace AquariumGame.Features.Fish.Scripts.Services.Factory
{
    public readonly struct FishInstance
    {
        public readonly GameObject Root;
        public readonly IFishFacade Facade;

        public FishInstance(GameObject root, IFishFacade facade)
        {
            Root = root;
            Facade = facade;
        }
    }
}
