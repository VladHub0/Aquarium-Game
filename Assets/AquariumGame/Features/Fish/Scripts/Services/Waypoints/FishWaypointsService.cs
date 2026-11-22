using AquariumGame.Features.Fish.Scripts.Services.Waypoints.Interface;
using UnityEngine;

namespace AquariumGame.Features.Fish.Scripts.Services.Waypoints
{
    public sealed class FishWaypointsService : IFishWaypointsService
    {
        private const float SamePointDistance = 0.01f;

        private IFishWaypoints _waypoints;

        public bool HasPoints => _waypoints != null && _waypoints.Count > 0;

        public void Register(IFishWaypoints waypoints)
        {
            _waypoints = waypoints;
        }

        public void Unregister(IFishWaypoints waypoints)
        {
            if (ReferenceEquals(_waypoints, waypoints))
                _waypoints = null;
        }

        public bool TryGetRandomPoint(Vector3 current, out Vector3 point)
        {
            point = current;

            if (!HasPoints)
                return false;

            var count = _waypoints.Count;
            if (count == 1)
            {
                point = _waypoints.GetPoint(0);
                return true;
            }

            var index = Random.Range(0, count);
            if (Vector3.Distance(_waypoints.GetPoint(index), current) < SamePointDistance)
                index = (index + Random.Range(1, count)) % count;

            point = _waypoints.GetPoint(index);
            return true;
        }
    }
}
