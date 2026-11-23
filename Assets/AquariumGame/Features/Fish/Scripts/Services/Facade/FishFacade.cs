using AquariumGame.Features.Fish.Scripts.Presenter.Interface;
using AquariumGame.Features.Fish.Scripts.Services.Facade.Interface;
using AquariumGame.Features.Fish.Scripts.View.Interface;
using AquariumGame.Features.FishVitals.Scripts.Model.Interface;
using AquariumGame.Features.FishVitals.Scripts.Presenter.Interface;
using AquariumGame.Features.FishVitals.Scripts.Services.Interface;
using UnityEngine;

namespace AquariumGame.Features.Fish.Scripts.Services.Facade
{
    public sealed class FishFacade : IFishFacade
    {
        private readonly IFishView _view;
        private readonly IFishMovementPresenter _movement;
        private readonly IFishVitalsModel _vitals;
        private readonly IFishVitalsService _vitalsService;
        private readonly IFishVitalsPresenter _vitalsPresenter;

        private bool _disposed;

        public FishFacade(
            IFishView view,
            IFishMovementPresenter movement,
            IFishVitalsModel vitals,
            IFishVitalsService vitalsService,
            IFishVitalsPresenter vitalsPresenter)
        {
            _view = view;
            _movement = movement;
            _vitals = vitals;
            _vitalsService = vitalsService;
            _vitalsPresenter = vitalsPresenter;
        }

        public bool IsAlive => !_disposed && !_vitals.IsDead;

        public void Init() => _vitalsPresenter.Init();

        public void Tick(float deltaTime)
        {
            if (_disposed)
                return;

            _vitalsService.Tick(deltaTime);

            if (!_vitals.IsDead)
                _movement.Tick(deltaTime);
        }

        public bool ContainsPoint(Vector2 worldPoint) => !_disposed && _view.ContainsPoint(worldPoint);

        public void Tap() => _view.PlayTapFeedback();

        public void Dispose()
        {
            if (_disposed)
                return;

            _disposed = true;
            _movement.Dispose();
            _vitalsPresenter.Dispose();
        }
    }
}
