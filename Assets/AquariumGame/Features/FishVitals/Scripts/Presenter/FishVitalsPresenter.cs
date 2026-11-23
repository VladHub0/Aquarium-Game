using AquariumGame.Features.FishVitals.Scripts.Model.Interface;
using AquariumGame.Features.FishVitals.Scripts.Presenter.Interface;
using AquariumGame.Features.FishVitals.Scripts.Services.Interface;
using AquariumGame.Features.FishVitals.Scripts.View.Interface;

namespace AquariumGame.Features.FishVitals.Scripts.Presenter
{
    public sealed class FishVitalsPresenter : IFishVitalsPresenter
    {
        private readonly IFishVitalsModel _model;
        private readonly IFishVitalsService _service;
        private readonly IFishVitalsView _view;

        public FishVitalsPresenter(IFishVitalsModel model, IFishVitalsService service, IFishVitalsView view)
        {
            _model = model;
            _service = service;
            _view = view;
        }

        public void Init()
        {
            _model.Changed += Refresh;
            _model.Died += OnDied;
            Refresh();
        }

        public void Dispose()
        {
            _model.Changed -= Refresh;
            _model.Died -= OnDied;
        }

        private void Refresh()
        {
            _view.SetHealth(_model.HealthNormalized);
            _view.SetSaturation(_model.SaturationNormalized, _service.IsHungry);
        }

        private void OnDied() => _view.SetVisible(false);
    }
}
