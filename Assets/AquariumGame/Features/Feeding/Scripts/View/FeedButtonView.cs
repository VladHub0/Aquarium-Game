using System;
using AquariumGame.Features.Feeding.Scripts.View.Interface;
using AquariumGame.Features.UI.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace AquariumGame.Features.Feeding.Scripts.View
{
    public sealed class FeedButtonView : MonoBehaviour, IFeedButtonView
    {
        [SerializeField] private Sprite foodSprite;
        [SerializeField] private string idleLabel = "Feed";
        [SerializeField] private string activeLabel = "Tap aquarium";
        [SerializeField] private Color idleColor = Color.white;
        [SerializeField] private Color activeColor = new Color(1f, 0.8f, 0.35f, 1f);
        [SerializeField] private Color labelColor = new Color(0.15f, 0.15f, 0.2f, 1f);

        private Button _button;
        private Image _background;
        private Text _label;

        public event Action Clicked;

        private void Awake()
        {
            UiBuilder.EnsureEventSystem(transform);

            var canvas = UiBuilder.CreateCanvas(transform, "FeedCanvas", 10);

            _button = UiBuilder.CreateButton(canvas, "FeedButton");
            var rect = (RectTransform)_button.transform;
            rect.anchorMin = rect.anchorMax = rect.pivot = new Vector2(0f, 0f);
            rect.sizeDelta = new Vector2(340f, 90f);
            rect.anchoredPosition = new Vector2(30f, 30f);

            _background = _button.GetComponent<Image>();

            _label = _button.GetComponentInChildren<Text>();
            if (_label != null)
            {
                _label.rectTransform.offsetMin = new Vector2(96f, 0f);
                _label.rectTransform.offsetMax = new Vector2(-16f, 0f);
                UiBuilder.SetupText(_label, 34, TextAnchor.MiddleLeft, labelColor);
            }

            UiBuilder.CreateIcon(rect, foodSprite, new Color(0.75f, 0.45f, 0.2f, 1f), new Vector2(0f, 0.5f), new Vector2(50f, 0f), 60f);

            _button.onClick.AddListener(OnClicked);
            SetActive(false);
        }

        private void OnDestroy()
        {
            if (_button != null)
                _button.onClick.RemoveListener(OnClicked);
        }

        public void SetActive(bool active)
        {
            if (_background != null)
                _background.color = active ? activeColor : idleColor;

            if (_label != null)
                _label.text = active ? activeLabel : idleLabel;
        }

        private void OnClicked() => Clicked?.Invoke();
    }
}
