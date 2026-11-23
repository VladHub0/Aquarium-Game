using System;
using AquariumGame.Features.Wallet.Scripts.Model.Interface;
using AquariumGame.Features.Wallet.Scripts.View.Interface;
using Zenject;

namespace AquariumGame.Features.Wallet.Scripts.Presenter
{
    public sealed class CoinsPresenter : IInitializable, IDisposable
    {
        private readonly ICoinsView _view;
        private readonly IWalletModel _model;

        public CoinsPresenter(ICoinsView view, IWalletModel model)
        {
            _view = view;
            _model = model;
        }

        public void Initialize()
        {
            _model.Changed += OnCoinsChanged;
            _view.SetCoins(_model.Coins);
        }

        public void Dispose() => _model.Changed -= OnCoinsChanged;

        private void OnCoinsChanged(int coins) => _view.SetCoins(coins);
    }
}
