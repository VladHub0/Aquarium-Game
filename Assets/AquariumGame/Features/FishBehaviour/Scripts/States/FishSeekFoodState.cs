using AquariumGame.Features.Feeding.Scripts.Configs.Interface;
using AquariumGame.Features.Feeding.Scripts.Services.Food.Interface;
using AquariumGame.Features.Fish.Scripts.Model.Interface;
using AquariumGame.Features.Fish.Scripts.Presenter.Interface;
using AquariumGame.Features.Fish.Scripts.View.Interface;
using AquariumGame.Features.FishBehaviour.Scripts.Configs.Interface;
using AquariumGame.Features.FishBehaviour.Scripts.Core.Interface;
using AquariumGame.Features.FishVitals.Scripts.Model.Interface;
using AquariumGame.Features.FishVitals.Scripts.Services.Interface;

namespace AquariumGame.Features.FishBehaviour.Scripts.States
{
    public sealed class FishSeekFoodState : IFishState
    {
        private readonly IFishStateMachine _stateMachine;
        private readonly IFishMovementPresenter _movement;
        private readonly IFishMovementModel _movementModel;
        private readonly IFishView _view;
        private readonly IFishVitalsModel _vitals;
        private readonly IFishVitalsService _vitalsService;
        private readonly IFoodService _food;
        private readonly IFishBehaviourConfig _config;
        private readonly IFeedingConfig _feedingConfig;

        private IFoodItem _target;

        public FishSeekFoodState(
            IFishStateMachine stateMachine,
            IFishMovementPresenter movement,
            IFishMovementModel movementModel,
            IFishView view,
            IFishVitalsModel vitals,
            IFishVitalsService vitalsService,
            IFoodService food,
            IFishBehaviourConfig config,
            IFeedingConfig feedingConfig)
        {
            _stateMachine = stateMachine;
            _movement = movement;
            _movementModel = movementModel;
            _view = view;
            _vitals = vitals;
            _vitalsService = vitalsService;
            _food = food;
            _config = config;
            _feedingConfig = feedingConfig;
        }

        public void Enter() => _target = null;

        public void Tick(float deltaTime)
        {
            if (_vitals.IsDead)
            {
                _stateMachine.Enter<FishDeadState>();
                return;
            }

            if (!_vitalsService.CanEat || !TryGetTarget())
            {
                _stateMachine.Enter<FishSwimState>();
                return;
            }

            var speed = _movementModel.Speed * _config.SeekSpeedMultiplier;
            if (!_movement.MoveTowards(_target.Position, speed, _feedingConfig.EatDistance, deltaTime))
                return;

            if (_target.TryConsume())
                _vitalsService.Feed(_target.Nutrition);

            _target = null;
        }

        public void Exit() => _target = null;

        private bool TryGetTarget()
        {
            if (_target != null && _target.IsAvailable)
                return true;

            return _food.TryGetNearest(_view.Position, out _target);
        }
    }
}
