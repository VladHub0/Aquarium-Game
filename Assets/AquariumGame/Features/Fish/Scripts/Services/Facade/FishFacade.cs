
using AquariumGame.Features.Fish.Scripts.Configs.Interface;
using AquariumGame.Features.Fish.Scripts.Model;
using AquariumGame.Features.Fish.Scripts.Model.Interface;
using AquariumGame.Features.Fish.Scripts.Services.Facade.Interface;
using UnityEngine;
using Zenject;


namespace AquariumGame.Features.Fish.Scripts.Services.Facade
{
    public class FishFacade : IFishFacade
    {
        private readonly IFishConfig _fishConfig;

        private readonly IFishVitalsModel _fishVitalsModel;
        private readonly IFishMovementModel _fishMovementModel;

        [Inject]
        public  FishFacade(IFishConfig fishConfig, IFishMovementModel fishMovementModel, IFishVitalsModel fishVitalsModel)
        {
            _fishConfig = fishConfig;
            _fishMovementModel = fishMovementModel;
            _fishVitalsModel = fishVitalsModel;
        }
        public void Init()
        {
            Debug.Log("Hello From FishFacade");

            Debug.Log("Fish Config + " + _fishConfig.Speed);

            Debug.Log("Fish Movement Model + " + _fishMovementModel.Speed);

            Debug.Log("Fish Vital Model + " + _fishVitalsModel.Health);

        }
    }
}