using AquariumGame.Features.FishVitals.Scripts.View.Interface;
using UnityEngine;

namespace AquariumGame.Features.FishVitals.Scripts.View
{
    public sealed class FishVitalsView : MonoBehaviour, IFishVitalsView
    {
        [SerializeField] private Vector3 offset = new Vector3(0f, 0.6f, 0f);
        [SerializeField, Min(0.1f)] private float width = 0.7f;
        [SerializeField, Min(0.01f)] private float height = 0.08f;
        [SerializeField, Min(0f)] private float spacing = 0.04f;
        [SerializeField] private int sortingOrder = 20;
        [SerializeField, Min(0f)] private float hungryBlinkSpeed = 4f;
        [SerializeField] private Color backgroundColor = new Color(0f, 0f, 0f, 0.6f);
        [SerializeField] private Color healthColor = new Color(0.3f, 0.9f, 0.35f, 1f);
        [SerializeField] private Color lowHealthColor = new Color(0.95f, 0.25f, 0.2f, 1f);
        [SerializeField] private Color saturationColor = new Color(1f, 0.7f, 0.15f, 1f);
        [SerializeField] private Color hungryColor = new Color(0.95f, 0.3f, 0.1f, 1f);

        private static Sprite _pixel;

        private Transform _root;
        private SpriteRenderer _healthFill;
        private SpriteRenderer _saturationFill;
        private bool _hungry;

        private static Sprite Pixel
        {
            get
            {
                if (_pixel != null)
                    return _pixel;

                var texture = Texture2D.whiteTexture;
                _pixel = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(0f, 0.5f), texture.width);
                return _pixel;
            }
        }

        private void Awake()
        {
            var source = GetComponentInChildren<SpriteRenderer>();
            var material = source != null ? source.sharedMaterial : null;

            _root = new GameObject("Vitals").transform;
            _root.SetParent(transform, false);

            _healthFill = CreateBar("Health", 0f, material);
            _saturationFill = CreateBar("Saturation", -(height + spacing), material);

            SetHealth(1f);
            SetSaturation(1f, false);
        }

        private void LateUpdate()
        {
            if (_root == null)
                return;

            _root.SetPositionAndRotation(transform.position + offset, Quaternion.identity);

            if (!_hungry)
                return;

            var blink = Mathf.PingPong(Time.time * hungryBlinkSpeed, 1f);
            _saturationFill.color = Color.Lerp(hungryColor, saturationColor, blink);
        }

        public void SetHealth(float normalized)
        {
            if (_healthFill == null)
                return;

            normalized = Mathf.Clamp01(normalized);
            SetFill(_healthFill, normalized);
            _healthFill.color = Color.Lerp(lowHealthColor, healthColor, normalized);
        }

        public void SetSaturation(float normalized, bool hungry)
        {
            if (_saturationFill == null)
                return;

            _hungry = hungry;
            SetFill(_saturationFill, Mathf.Clamp01(normalized));

            if (!hungry)
                _saturationFill.color = saturationColor;
        }

        public void SetVisible(bool visible)
        {
            if (_root != null)
                _root.gameObject.SetActive(visible);
        }

        private SpriteRenderer CreateBar(string name, float y, Material material)
        {
            var bar = new GameObject(name).transform;
            bar.SetParent(_root, false);
            bar.localPosition = new Vector3(-width * 0.5f, y, 0f);

            var background = CreateRenderer("Background", bar, material, sortingOrder);
            background.color = backgroundColor;
            background.transform.localScale = new Vector3(width, height, 1f);

            var fill = CreateRenderer("Fill", bar, material, sortingOrder + 1);
            fill.transform.localScale = new Vector3(width, height, 1f);
            return fill;
        }

        private static SpriteRenderer CreateRenderer(string name, Transform parent, Material material, int order)
        {
            var go = new GameObject(name, typeof(SpriteRenderer));
            go.transform.SetParent(parent, false);

            var renderer = go.GetComponent<SpriteRenderer>();
            renderer.sprite = Pixel;
            renderer.sortingOrder = order;

            if (material != null)
                renderer.sharedMaterial = material;

            return renderer;
        }

        private void SetFill(SpriteRenderer fill, float normalized)
        {
            fill.transform.localScale = new Vector3(width * normalized, height, 1f);
        }
    }
}
