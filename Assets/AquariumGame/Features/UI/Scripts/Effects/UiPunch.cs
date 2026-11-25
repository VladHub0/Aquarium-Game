using UnityEngine;

namespace AquariumGame.Features.UI.Scripts.Effects
{
    public sealed class UiPunch : MonoBehaviour
    {
        [SerializeField, Min(0f)] private float strength = 0.18f;
        [SerializeField, Min(0.01f)] private float duration = 0.25f;

        private float _elapsed = -1f;

        public void Play()
        {
            _elapsed = 0f;
            enabled = true;
        }

        private void OnDisable() => transform.localScale = Vector3.one;

        private void Update()
        {
            if (_elapsed < 0f)
                return;

            _elapsed += Time.unscaledDeltaTime;
            var t = Mathf.Clamp01(_elapsed / duration);
            var scale = 1f + Mathf.Sin(t * Mathf.PI) * strength * (1f - t * 0.5f);
            transform.localScale = new Vector3(scale, scale, 1f);

            if (t < 1f)
                return;

            _elapsed = -1f;
            transform.localScale = Vector3.one;
        }
    }
}
