using System.Collections.Generic;
using AquariumGame.Features.Fish.Scripts.Services.Waypoints.Interface;
using UnityEngine;

namespace AquariumGame.Features.Fish.Scripts.Services.Waypoints
{
    public sealed class FishWaypointsService : IFishWaypointsService
    {
        private const float SamePointDistance = 0.01f;

        private readonly Dictionary<object, int> _reservations = new Dictionary<object, int>();
        private readonly List<int> _candidates = new List<int>();

        private IFishWaypoints _waypoints;
        private int[] _occupancy;

        public bool HasPoints => _waypoints != null && _waypoints.Count > 0;

        public void Register(IFishWaypoints waypoints)
        {
            _waypoints = waypoints;
            ResetReservations();
        }

        public void Unregister(IFishWaypoints waypoints)
        {
            if (!ReferenceEquals(_waypoints, waypoints))
                return;

            _waypoints = null;
            ResetReservations();
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

        public bool TryReservePoint(object owner, Vector3 current, out Vector3 point)
        {
            point = current;

            if (!HasPoints)
                return false;

            var count = _waypoints.Count;
            EnsureOccupancy(count);

            var previous = ReleaseIndex(owner);
            var index = PickLeastOccupied(count, previous, current);

            _occupancy[index]++;
            _reservations[owner] = index;
            point = _waypoints.GetPoint(index);
            return true;
        }

        public void Release(object owner) => ReleaseIndex(owner);

        private int PickLeastOccupied(int count, int previous, Vector3 current)
        {
            _candidates.Clear();
            var min = int.MaxValue;

            for (var i = 0; i < count; i++)
            {
                if (count > 1 && (i == previous || Vector3.Distance(_waypoints.GetPoint(i), current) < SamePointDistance))
                    continue;

                var occupancy = _occupancy[i];
                if (occupancy > min)
                    continue;

                if (occupancy < min)
                {
                    min = occupancy;
                    _candidates.Clear();
                }

                _candidates.Add(i);
            }

            if (_candidates.Count == 0)
                return previous >= 0 ? previous : 0;

            return _candidates[Random.Range(0, _candidates.Count)];
        }

        private int ReleaseIndex(object owner)
        {
            if (owner == null || !_reservations.TryGetValue(owner, out var index))
                return -1;

            _reservations.Remove(owner);

            if (_occupancy != null && index < _occupancy.Length && _occupancy[index] > 0)
                _occupancy[index]--;

            return index;
        }

        private void EnsureOccupancy(int count)
        {
            if (_occupancy != null && _occupancy.Length == count)
                return;

            _occupancy = new int[count];
            _reservations.Clear();
        }

        private void ResetReservations()
        {
            _reservations.Clear();
            _occupancy = null;
        }
    }
}
