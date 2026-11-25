using System;
using System.Threading;
using AquariumGame.Features.Fish.Scripts.Catalog;
using AquariumGame.Features.Shop.Scripts.Services.Interface;
using AquariumGame.Features.Shop.Scripts.View.Interface;
using AquariumGame.Features.Wallet.Scripts.Model.Interface;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace AquariumGame.Features.Shop.Scripts.Presenter
{
    public sealed class ShopPresenter : IInitializable, IDisposable
    {
        private readonly IShopView _view;
        private readonly IFishShop _shop;
        private readonly IWalletModel _wallet;
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();

        private FishSpecies[] _items = Array.Empty<FishSpecies>();

        public ShopPresenter(IShopView view, IFishShop shop, IWalletModel wallet)
        {
            _view = view;
            _shop = shop;
            _wallet = wallet;
        }

        public void Initialize()
        {
            _items = FishCatalog.All;

            foreach (var species in _items)
                _view.AddItem(species.Key, species.Price);

            _view.BuyClicked += OnBuyClicked;
            _wallet.Changed += OnCoinsChanged;

            Refresh();
        }

        public void Dispose()
        {
            _view.BuyClicked -= OnBuyClicked;
            _wallet.Changed -= OnCoinsChanged;

            _cts.Cancel();
            _cts.Dispose();
        }

        private void OnBuyClicked(int index)
        {
            if (index < 0 || index >= _items.Length)
                return;

            BuyAsync(_items[index]).Forget();
        }

        private async UniTaskVoid BuyAsync(FishSpecies species)
        {
            try
            {
                await _shop.TryBuyAsync(species, _cts.Token);
            }
            catch (OperationCanceledException)
            {
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        private void OnCoinsChanged(int coins) => Refresh();

        private void Refresh()
        {
            for (var i = 0; i < _items.Length; i++)
                _view.SetItemInteractable(i, _shop.CanAfford(_items[i]));
        }
    }
}
