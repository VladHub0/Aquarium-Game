using UnityEngine;

namespace AquariumGame.Features.UI.Scripts.Effects
{
    public sealed class UiPulse : MonoBehaviour
    {
        [SerializeField, Min(0f)] private float amplitude = 0.06f;
        [SerializeField, Min(0.01f)] private float frequency = 1.6f;

        private float _time;

        public void Setup(float pulseAmplitude, float pulseFrequency)
        {
            amplitude = pulseAmplitude;
            frequency = pulseFrequency;
        }

        private void OnEnable() => _time = 0f;

        private void OnDisable() => transform.localScale = Vector3.one;

        private void Update()
        {
            _time += Time.unscaledDeltaTime;
            var scale = 1f + Mathf.Sin(_time * frequency * Mathf.PI * 2f) * amplitude;
            transform.localScale = new Vector3(scale, scale, 1f);
        }
    }
}
