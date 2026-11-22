using UnityEngine;

namespace AquariumGame.Features.Fish.Scripts.Services.Waypoints.Interface
{
    public interface IFishWaypoints
    {
        int Count { get; }

        Vector3 GetPoint(int index);
    }
}
