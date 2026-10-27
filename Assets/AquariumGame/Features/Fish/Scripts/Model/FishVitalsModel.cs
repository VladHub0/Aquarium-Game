
using AquariumGame.Features.Fish.Scripts.Configs.Interface;
using Zenject;

namespace AquariumGame.Features.Fish.Scripts.Model
{
    public class FishVitalsModel : IFishVitalsModel
    {
        private readonly IFishConfig _fishConfig;

        public float MaxHealth { get; set; }
        public float Health { get; set; }
        public float MaxSaturation { get; set; }
        public float Saturation { get; set; }
        public float SaturationTick { get; set; }

        [Inject]
        public FishVitalsModel(IFishConfig fishConfig)
        {
            _fishConfig = fishConfig;

            Init();
        }

        private void Init()
        {
            MaxHealth = _fishConfig.Health;
            Health = _fishConfig.Health;

            MaxSaturation = _fishConfig.Saturation;
            Saturation = _fishConfig.Saturation;

            SaturationTick = _fishConfig.SaturationTick;

        }
        
    }
}
