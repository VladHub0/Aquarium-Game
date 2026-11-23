using System;
using AquariumGame.Features.Fish.Scripts.Services.Registry.Interface;
using AquariumGame.Features.FishTap.Scripts.Configs.Interface;
using AquariumGame.Features.PointerInput.Scripts.Services.Interface;
using AquariumGame.Features.Wallet.Scripts.Model.Interface;
using UnityEngine;
using Zenject;

namespace AquariumGame.Features.FishTap.Scripts.Services
{
    public sealed class FishTapHandler : ITapHandler, IInitializable, IDisposable
    {
        private const int Priority = 0;

        private readonly IPointerInputService _input;
        private readonly IFishRegistry _registry;
        private readonly IWalletModel _wallet;
        private readonly IFishTapConfig _config;

        public FishTapHandler(IPointerInputService input, IFishRegistry registry, IWalletModel wallet, IFishTapConfig config)
        {
            _input = input;
            _registry = registry;
            _wallet = wallet;
            _config = config;
        }

        public void Initialize() => _input.Register(this, Priority);

        public void Dispose() => _input.Unregister(this);

        public bool TryHandleTap(Vector2 worldPoint)
        {
            var fish = _registry.All;

            for (var i = fish.Count - 1; i >= 0; i--)
            {
                var item = fish[i];
                if (!item.IsAlive || !item.ContainsPoint(worldPoint))
                    continue;

                _wallet.Add(_config.CoinsPerTap);
                item.Tap();
                return true;
            }

            return false;
        }
    }
}
