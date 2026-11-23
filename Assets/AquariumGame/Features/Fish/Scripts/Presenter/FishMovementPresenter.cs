using AquariumGame.Features.Fish.Scripts.Configs.Interface;
using AquariumGame.Features.Fish.Scripts.Model.Interface;
using AquariumGame.Features.Fish.Scripts.Presenter.Interface;
using AquariumGame.Features.Fish.Scripts.Services.Waypoints.Interface;
using AquariumGame.Features.Fish.Scripts.View.Interface;
using UnityEngine;

namespace AquariumGame.Features.Fish.Scripts.Presenter
{
    public sealed class FishMovementPresenter : IFishMovementPresenter
    {
        private readonly IFishView _view;
        private readonly IFishMovementModel _model;
        private readonly IFishConfig _config;
        private readonly IFishWaypointsService _waypoints;

        public FishMovementPresenter(
            IFishView view,
            IFishMovementModel model,
            IFishConfig config,
            IFishWaypointsService waypoints)
        {
            _view = view;
            _model = model;
            _config = config;
            _waypoints = waypoints;
        }

        public void Tick(float deltaTime)
        {
            if (!_model.HasTarget && !TrySelectNextTarget())
                return;

            if (MoveTowards(_model.Target, _model.Speed, _config.WaypointReachDistance, deltaTime))
                TrySelectNextTarget();
        }

        public bool MoveTowards(Vector3 target, float speed, float reachDistance, float deltaTime)
        {
            var position = _view.Position;
            target.z = position.z;

            var next = Vector3.MoveTowards(position, target, speed * deltaTime);

            _view.LookTo(target - position, deltaTime);
            _view.Position = next;

            return Vector3.Distance(next, target) <= reachDistance;
        }

        public void Stop()
        {
            _waypoints.Release(this);
            _model.HasTarget = false;
        }

        public void Dispose() => Stop();

        private bool TrySelectNextTarget()
        {
            if (!_waypoints.TryReservePoint(this, _view.Position, out var point))
            {
                _model.HasTarget = false;
                return false;
            }

            point.z = _view.Position.z;
            _model.Target = point;
            _model.HasTarget = true;
            return true;
        }
    }
}
