using UnityEngine;

namespace AquariumGame.Features.Feeding.Scripts.Configs.Interface
{
    public interface IFeedingConfig
    {
        Sprite FoodSprite { get; }

        Material FoodMaterial { get; }

        Color FoodColor { get; }

        float FoodScale { get; }

        int SortingOrder { get; }

        int PelletsPerTap { get; }

        float ScatterRadius { get; }

        float Nutrition { get; }

        float SinkSpeed { get; }

        float BottomY { get; }

        float Lifetime { get; }

        float EatDistance { get; }
    }
}
