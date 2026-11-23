using AquariumGame.Features.Feeding.Scripts.Services.Food.Interface;
using AquariumGame.Features.Feeding.Scripts.View.Interface;
using UnityEngine;

namespace AquariumGame.Features.Feeding.Scripts.Services.Food
{
    public sealed class FoodItem : IFoodItem
    {
        private readonly IFoodView _view;
        private float _lifetime;

        public FoodItem(IFoodView view, float nutrition, float lifetime)
        {
            _view = view;
            _lifetime = lifetime;
            Nutrition = nutrition;
            Position = view.Position;
        }

        public Vector3 Position { get; private set; }

        public float Nutrition { get; }

        public bool IsAvailable { get; private set; } = true;

        public bool TryConsume()
        {
            if (!IsAvailable)
                return false;

            IsAvailable = false;
            return true;
        }

        public void Tick(float deltaTime, float sinkSpeed, float bottomY)
        {
            if (!IsAvailable)
                return;

            _lifetime -= deltaTime;
            if (_lifetime <= 0f)
            {
                IsAvailable = false;
                return;
            }

            var position = Position;
            position.y = Mathf.Max(bottomY, position.y - sinkSpeed * deltaTime);
            Position = position;
            _view.Position = position;
        }

        public void Destroy() => _view.Destroy();
    }
}
