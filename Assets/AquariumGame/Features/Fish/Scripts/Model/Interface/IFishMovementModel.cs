
using UnityEngine;

namespace AquariumGame.Features.Fish.Scripts.Model.Interface
{
    public interface IFishMovementModel
    {
        float Speed { get; set; }

        Vector3 Target { get; set; }

        bool HasTarget { get; set; }
    }
}
