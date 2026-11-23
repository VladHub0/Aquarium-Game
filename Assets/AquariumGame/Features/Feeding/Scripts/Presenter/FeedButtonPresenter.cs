using System;
using AquariumGame.Features.Feeding.Scripts.Model.Interface;
using AquariumGame.Features.Feeding.Scripts.View.Interface;
using Zenject;

namespace AquariumGame.Features.Feeding.Scripts.Presenter
{
    public sealed class FeedButtonPresenter : IInitializable, IDisposable
    {
        private readonly IFeedButtonView _view;
        private readonly IFeedingModeModel _model;

        public FeedButtonPresenter(IFeedButtonView view, IFeedingModeModel model)
        {
            _view = view;
            _model = model;
        }

        public void Initialize()
        {
            _view.Clicked += OnClicked;
            _model.Changed += OnModeChanged;
            _view.SetActive(_model.IsActive);
        }

        public void Dispose()
        {
            _view.Clicked -= OnClicked;
            _model.Changed -= OnModeChanged;
            _model.SetActive(false);
        }

        private void OnClicked() => _model.Toggle();

        private void OnModeChanged(bool active) => _view.SetActive(active);
    }
}
