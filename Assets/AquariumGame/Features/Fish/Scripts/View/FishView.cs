using AquariumGame.Features.Fish.Scripts.View.Interface;
using UnityEngine;

namespace AquariumGame.Features.Fish.Scripts.View
{
    public sealed class FishView : MonoBehaviour, IFishView
    {
        private const float MinDirectionSqr = 0.000001f;

        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField, Min(0f)] private float turnSpeed = 360f;
        [SerializeField, Min(0f)] private float tapPunch = 0.25f;
        [SerializeField, Min(0.01f)] private float tapDuration = 0.25f;
        [SerializeField] private Color deadColor = new Color(0.6f, 0.6f, 0.6f, 0.85f);

        private Vector3 _baseScale;
        private float _tapTime = -1f;

        public Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }

        private void Awake() => _baseScale = transform.localScale;

        private void Update()
        {
            if (_tapTime < 0f)
                return;

            _tapTime += Time.deltaTime;
            var t = Mathf.Clamp01(_tapTime / tapDuration);
            var punch = Mathf.Sin(t * Mathf.PI) * tapPunch;
            transform.localScale = _baseScale * (1f + punch);

            if (t >= 1f)
            {
                transform.localScale = _baseScale;
                _tapTime = -1f;
            }
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

        public bool ContainsPoint(Vector2 worldPoint)
        {
            if (_spriteRenderer == null)
                return false;

            var bounds = _spriteRenderer.bounds;
            return worldPoint.x >= bounds.min.x && worldPoint.x <= bounds.max.x
                && worldPoint.y >= bounds.min.y && worldPoint.y <= bounds.max.y;
        }

        public void PlayTapFeedback() => _tapTime = 0f;

        public void SetDead()
        {
            _tapTime = -1f;
            transform.localScale = _baseScale;
            transform.rotation = Quaternion.identity;

            var animator = GetComponent<Animator>();
            if (animator != null)
                animator.speed = 0f;

            if (_spriteRenderer == null)
                return;

            _spriteRenderer.flipX = false;
            _spriteRenderer.flipY = true;
            _spriteRenderer.color = deadColor;
        }
    }
}
