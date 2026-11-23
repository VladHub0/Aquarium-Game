using UnityEngine;

namespace AquariumGame.Features.Feeding.Scripts.View.Interface
{
    public interface IFoodView
    {
        Vector3 Position { get; set; }

        void Destroy();
    }
}
