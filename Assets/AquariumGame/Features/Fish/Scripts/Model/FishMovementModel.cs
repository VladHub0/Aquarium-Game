
using AquariumGame.Features.Fish.Scripts.Configs.Interface;
using AquariumGame.Features.Fish.Scripts.Model.Interface;

using Zenject;

namespace AquariumGame.Features.Fish.Scripts.Model
{
    public class FishMovementModel : IFishMovementModel
    {
        private readonly IFishConfig _fishConfig;

        [Inject]
        public FishMovementModel(IFishConfig fishConfig)
        {
            _fishConfig = fishConfig;
            Init();
        }

        private void Init()
        {
            Speed = _fishConfig.Speed;
        }
        public float Speed { get; set; }
    }
}
