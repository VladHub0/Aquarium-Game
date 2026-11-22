using UnityEngine;

namespace AquariumGame.Features.Fish.Scripts.Services.Waypoints.Interface
{
    public interface IFishWaypointsService
    {
        bool HasPoints { get; }

        void Register(IFishWaypoints waypoints);

        void Unregister(IFishWaypoints waypoints);

        bool TryGetRandomPoint(Vector3 current, out Vector3 point);

        bool TryReservePoint(object owner, Vector3 current, out Vector3 point);

        void Release(object owner);
    }
}
