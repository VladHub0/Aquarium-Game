using AquariumGame.Features.Feeding.Scripts.Services.Food.Interface;
using AquariumGame.Features.Fish.Scripts.Presenter.Interface;
using AquariumGame.Features.FishBehaviour.Scripts.Core.Interface;
using AquariumGame.Features.FishVitals.Scripts.Model.Interface;
using AquariumGame.Features.FishVitals.Scripts.Services.Interface;

namespace AquariumGame.Features.FishBehaviour.Scripts.States
{
    public sealed class FishSwimState : IFishState
    {
        private readonly IFishStateMachine _stateMachine;
        private readonly IFishMovementPresenter _movement;
        private readonly IFishVitalsModel _vitals;
        private readonly IFishVitalsService _vitalsService;
        private readonly IFoodService _food;

        public FishSwimState(
            IFishStateMachine stateMachine,
            IFishMovementPresenter movement,
            IFishVitalsModel vitals,
            IFishVitalsService vitalsService,
            IFoodService food)
        {
            _stateMachine = stateMachine;
            _movement = movement;
            _vitals = vitals;
            _vitalsService = vitalsService;
            _food = food;
        }

        public void Enter()
        {
        }

        public void Tick(float deltaTime)
        {
            if (_vitals.IsDead)
            {
                _stateMachine.Enter<FishDeadState>();
                return;
            }

            if (_vitalsService.CanEat && _food.HasFood)
            {
                _stateMachine.Enter<FishSeekFoodState>();
                return;
            }

            _movement.Tick(deltaTime);
        }

        public void Exit() => _movement.Stop();
    }
}
