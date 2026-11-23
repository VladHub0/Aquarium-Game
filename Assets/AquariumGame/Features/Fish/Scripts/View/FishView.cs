using AquariumGame.Features.Fish.Scripts.View.Interface;
using UnityEngine;

namespace AquariumGame.Features.Fish.Scripts.View
{
    public sealed class FishView : MonoBehaviour, IFishView
    {
        private const float MinDirectionSqr = 0.000001f;

        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField, Min(0f)] private float turnSpeed = 360f;

        public Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }

        public void LookTo(Vector3 direction, float deltaTime)
        {
            if (new Vector2(direction.x, direction.y).sqrMagnitude < MinDirectionSqr)
                return;

            var targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            var angle = Mathf.MoveTowardsAngle(transform.eulerAngles.z, targetAngle, turnSpeed * deltaTime);
            transform.rotation = Quaternion.Euler(0f, 0f, angle);

            if (_spriteRenderer == null)
                return;

            var normalized = Mathf.Repeat(angle, 360f);
            _spriteRenderer.flipX = false;
            _spriteRenderer.flipY = normalized > 90f && normalized < 270f;
        }
    }
}
