using AquariumGame.Features.Feeding.Scripts.View.Interface;
using UnityEngine;

namespace AquariumGame.Features.Feeding.Scripts.Services.Factory.Interface
{
    public interface IFoodFactory
    {
        IFoodView Create(Vector3 position);
    }
}
