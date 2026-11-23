using AquariumGame.Features.FishVitals.Scripts.Configs.Interface;
using AquariumGame.Features.FishVitals.Scripts.Model.Interface;
using AquariumGame.Features.FishVitals.Scripts.Services.Interface;

namespace AquariumGame.Features.FishVitals.Scripts.Services
{
    public sealed class FishVitalsService : IFishVitalsService
    {
        private readonly IFishVitalsModel _model;
        private readonly IFishVitalsConfig _config;

        public FishVitalsService(IFishVitalsModel model, IFishVitalsConfig config)
        {
            _model = model;
            _config = config;
        }

        public bool IsHungry => !_model.IsDead && _model.SaturationNormalized < _config.HungryThreshold;

        public bool CanEat => !_model.IsDead && _model.SaturationNormalized < _config.FullThreshold;

        public void Tick(float deltaTime)
        {
            if (_model.IsDead || deltaTime <= 0f)
                return;

            _model.ChangeSaturation(-_config.SaturationDecayPerSecond * deltaTime);

            if (_model.Saturation <= 0f)
                _model.ChangeHealth(-_config.StarvationDamagePerSecond * deltaTime);
            else if (!IsHungry)
                _model.ChangeHealth(_config.RegenerationPerSecond * deltaTime);
        }

        public void Feed(float nutrition)
        {
            if (nutrition > 0f)
                _model.ChangeSaturation(nutrition);
        }
    }
}
