using UnityEngine;

namespace AquariumGame.Features.Feeding.Scripts.Services.Food.Interface
{
    public interface IFoodService
    {
        bool HasFood { get; }

        void Spawn(Vector2 point);

        bool TryGetNearest(Vector3 from, out IFoodItem food);

        void Clear();
    }
}
