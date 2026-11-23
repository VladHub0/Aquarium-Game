using UnityEngine;

namespace AquariumGame.Features.Fish.Scripts.Services.Factory.Interface
{
    public interface IFishFactory
    {
        FishInstance Create(GameObject prefab, Vector3 position);
    }
}
