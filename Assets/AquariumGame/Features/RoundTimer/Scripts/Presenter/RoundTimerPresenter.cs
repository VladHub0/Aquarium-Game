using System;
using AquariumGame.Features.RoundTimer.Scripts.Configs.Interface;
using AquariumGame.Features.RoundTimer.Scripts.Model.Interface;
using AquariumGame.Features.RoundTimer.Scripts.View.Interface;
using UnityEngine;
using Zenject;

namespace AquariumGame.Features.RoundTimer.Scripts.Presenter
{
    public sealed class RoundTimerPresenter : IInitializable, IDisposable
    {
        private readonly IRoundTimerView _view;
        private readonly IRoundTimerModel _model;
        private readonly IRoundTimerConfig _config;

        public RoundTimerPresenter(IRoundTimerView view, IRoundTimerModel model, IRoundTimerConfig config)
        {
            _view = view;
            _model = model;
            _config = config;
        }

        public void Initialize()
        {
            _model.Changed += OnChanged;
            OnChanged(_model.Remaining);
        }

        public void Dispose() => _model.Changed -= OnChanged;

        private void OnChanged(float remaining)
        {
            _view.SetTime(Mathf.CeilToInt(remaining), remaining <= _config.WarningTime);
        }
    }
}
