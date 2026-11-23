using System;
using System.Collections.Generic;
using AquariumGame.Features.Feeding.Scripts.Configs.Interface;
using AquariumGame.Features.Feeding.Scripts.Services.Factory.Interface;
using AquariumGame.Features.Feeding.Scripts.Services.Food.Interface;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace AquariumGame.Features.Feeding.Scripts.Services.Food
{
    public sealed class FoodService : IFoodService, ITickable, IDisposable
    {
        private readonly IFeedingConfig _config;
        private readonly IFoodFactory _factory;
        private readonly List<FoodItem> _items = new List<FoodItem>();

        public FoodService(IFeedingConfig config, IFoodFactory factory)
        {
            _config = config;
            _factory = factory;
        }

        public bool HasFood
        {
            get
            {
                foreach (var item in _items)
                    if (item.IsAvailable)
                        return true;

                return false;
            }
        }

        public void Spawn(Vector2 point)
        {
            for (var i = 0; i < _config.PelletsPerTap; i++)
            {
                var offset = i == 0 ? Vector2.zero : Random.insideUnitCircle * _config.ScatterRadius;
                var position = new Vector3(point.x + offset.x, point.y + offset.y, 0f);
                var view = _factory.Create(position);
                _items.Add(new FoodItem(view, _config.Nutrition, _config.Lifetime));
            }
        }

        public bool TryGetNearest(Vector3 from, out IFoodItem food)
        {
            food = null;
            var best = float.MaxValue;

            foreach (var item in _items)
            {
                if (!item.IsAvailable)
                    continue;

                var delta = (Vector2)(item.Position - from);
                var distance = delta.sqrMagnitude;
                if (distance >= best)
                    continue;

                best = distance;
                food = item;
            }

            return food != null;
        }

        public void Tick()
        {
            if (_items.Count == 0)
                return;

            var deltaTime = Time.deltaTime;

            for (var i = _items.Count - 1; i >= 0; i--)
            {
                var item = _items[i];
                item.Tick(deltaTime, _config.SinkSpeed, _config.BottomY);

                if (item.IsAvailable)
                    continue;

                item.Destroy();
                _items.RemoveAt(i);
            }
        }

        public void Clear()
        {
            foreach (var item in _items)
                item.Destroy();

            _items.Clear();
        }

        public void Dispose() => Clear();
    }
}
