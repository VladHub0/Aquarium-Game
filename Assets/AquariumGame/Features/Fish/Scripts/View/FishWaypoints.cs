using AquariumGame.Features.Fish.Scripts.Services.Waypoints.Interface;
using UnityEngine;
using Zenject;

namespace AquariumGame.Features.Fish.Scripts.View
{
    public sealed class FishWaypoints : MonoBehaviour, IFishWaypoints
    {
        [SerializeField] private Transform[] _points;

        private IFishWaypointsService _service;

        public int Count => _points == null ? 0 : _points.Length;

        public Vector3 GetPoint(int index) => _points[index].position;

        [Inject]
        public void Construct([InjectOptional] IFishWaypointsService service)
        {
            _service = service;
            _service?.Register(this);
        }

        private void OnDestroy()
        {
            _service?.Unregister(this);
        }

        private void OnDrawGizmos()
        {
            if (_points == null)
                return;

            Gizmos.color = Color.yellow;
            for (var i = 0; i < _points.Length; i++)
            {
                if (_points[i] == null)
                    continue;

                Gizmos.DrawWireSphere(_points[i].position, 0.2f);
            }
        }
    }
}
