using UnityEngine;

namespace AquariumGame.Features.Feeding.Scripts.Services.Food.Interface
{
    public interface IFoodItem
    {
        Vector3 Position { get; }

        float Nutrition { get; }

        bool IsAvailable { get; }

        bool TryConsume();
    }
}
