using AquariumGame.Features.Fish.Scripts.Presenter.Interface;
using AquariumGame.Features.Fish.Scripts.Services.Facade.Interface;
using AquariumGame.Features.Fish.Scripts.Services.Spawner.Interface;
using AquariumGame.Features.Fish.Scripts.View.Interface;
using AquariumGame.Features.FishBehaviour.Scripts.Configs.Interface;
using AquariumGame.Features.FishBehaviour.Scripts.Core.Interface;
using UnityEngine;

namespace AquariumGame.Features.FishBehaviour.Scripts.States
{
    public sealed class FishDeadState : IFishState
    {
        private readonly IFishView _view;
        private readonly IFishMovementPresenter _movement;
        private readonly IFishBehaviourConfig _config;
        private readonly IFishSpawner _spawner;
        private readonly IFishFacade _facade;

        private float _elapsed;
        private bool _removed;

        public FishDeadState(
            IFishView view,
            IFishMovementPresenter movement,
            IFishBehaviourConfig config,
            IFishSpawner spawner,
            IFishFacade facade)
        {
            _view = view;
            _movement = movement;
            _config = config;
            _spawner = spawner;
            _facade = facade;
        }

        public void Enter()
        {
            _elapsed = 0f;
            _removed = false;
            _movement.Stop();
            _view.SetDead();
        }

        public void Tick(float deltaTime)
        {
            if (_removed)
                return;

            _elapsed += deltaTime;
            _view.Position += Vector3.up * (_config.DeadFloatSpeed * deltaTime);

            if (_elapsed < _config.DeadRemoveDelay)
                return;

            _removed = true;
            _spawner.Despawn(_facade);
        }

        public void Exit()
        {
        }
    }
}
