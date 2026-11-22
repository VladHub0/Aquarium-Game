using AquariumGame.Features.Fish.Scripts.Configs.Interface;
using AquariumGame.Features.Fish.Scripts.Model.Interface;
using AquariumGame.Features.Fish.Scripts.Services.Waypoints.Interface;
using AquariumGame.Features.Fish.Scripts.View.Interface;
using UnityEngine;
using Zenject;

namespace AquariumGame.Features.Fish.Scripts.Presenter
{
    public sealed class FishMovementPresenter : ITickable
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

        public void Tick()
        {
            if (!_model.HasTarget && !TrySelectNextTarget())
                return;

            var position = _view.Position;
            var target = _model.Target;
            var next = Vector3.MoveTowards(position, target, _model.Speed * Time.deltaTime);

            _view.LookTo(target.x - position.x);
            _view.Position = next;

            if (Vector3.Distance(next, target) <= _config.WaypointReachDistance)
                TrySelectNextTarget();
        }

        private bool TrySelectNextTarget()
        {
            if (!_waypoints.TryGetRandomPoint(_model.HasTarget ? _model.Target : _view.Position, out var point))
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
