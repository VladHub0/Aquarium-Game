using AquariumGame.Features.Feeding.Scripts.Configs.Interface;
using AquariumGame.Features.Feeding.Scripts.Services.Factory.Interface;
using AquariumGame.Features.Feeding.Scripts.View;
using AquariumGame.Features.Feeding.Scripts.View.Interface;
using UnityEngine;

namespace AquariumGame.Features.Feeding.Scripts.Services.Factory
{
    public sealed class FoodFactory : IFoodFactory
    {
        private readonly IFeedingConfig _config;

        public FoodFactory(IFeedingConfig config) => _config = config;

        public IFoodView Create(Vector3 position)
        {
            var go = new GameObject("Food", typeof(SpriteRenderer), typeof(FoodView));
            var view = go.GetComponent<FoodView>();

            view.Setup(_config.FoodSprite, _config.FoodMaterial, _config.FoodColor, _config.FoodScale, _config.SortingOrder);
            view.Position = position;

            return view;
        }
    }
}
