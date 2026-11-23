using System;
using AquariumGame.Features.Feeding.Scripts.Model.Interface;
using AquariumGame.Features.Feeding.Scripts.Services.Food.Interface;
using AquariumGame.Features.PointerInput.Scripts.Services.Interface;
using UnityEngine;
using Zenject;

namespace AquariumGame.Features.Feeding.Scripts.Services.Tap
{
    public sealed class FeedingTapHandler : ITapHandler, IInitializable, IDisposable
    {
        private const int Priority = 100;

        private readonly IPointerInputService _input;
        private readonly IFeedingModeModel _mode;
        private readonly IFoodService _food;

        public FeedingTapHandler(IPointerInputService input, IFeedingModeModel mode, IFoodService food)
        {
            _input = input;
            _mode = mode;
            _food = food;
        }

        public void Initialize() => _input.Register(this, Priority);

        public void Dispose() => _input.Unregister(this);

        public bool TryHandleTap(Vector2 worldPoint)
        {
            if (!_mode.IsActive)
                return false;

            _food.Spawn(worldPoint);
            _mode.SetActive(false);
            return true;
        }
    }
}
