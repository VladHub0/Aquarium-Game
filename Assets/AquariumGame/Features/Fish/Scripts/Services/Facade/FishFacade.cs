
using AquariumGame.Features.Fish.Scripts.Configs.Interface;
using AquariumGame.Features.Fish.Scripts.Services.Facade.Interface;
using UnityEngine;


namespace AquariumGame.Features.Fish.Scripts.Services.Facade
{
    public class FishFacade : IFishFacade
    {
        private readonly IFishConfig _fishConfig;
        private FishFacade(IFishConfig fishConfig)
        {
            _fishConfig = fishConfig;
        }
        public void Init()
        {
            Debug.Log("Hello From FishFacade");

            Debug.Log("Fish Config + " + _fishConfig.Speed);

        }
    }
}