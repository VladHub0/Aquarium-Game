using System;
using AquariumGame.Features.Feeding.Scripts.View.Interface;
using AquariumGame.Features.UI.Scripts;
using AquariumGame.Features.UI.Scripts.Effects;
using AquariumGame.Features.UI.Scripts.Skin;
using UnityEngine;
using UnityEngine.UI;

namespace AquariumGame.Features.Feeding.Scripts.View
{
    public sealed class FeedButtonView : MonoBehaviour, IFeedButtonView
    {
        [SerializeField] private UiSkin skin;
        [SerializeField] private Sprite foodSprite;
        [SerializeField] private string idleLabel = "FEED";
        [SerializeField] private string activeLabel = "TAP!";
        [SerializeField] private Vector2 position = new Vector2(100f, -70f);
        [SerializeField, Min(1f)] private float slotSize = 130f;
        [SerializeField] private Vector2 foodSize = new Vector2(84f, 84f);
        [SerializeField] private float tagOffsetY = -64f;
        [SerializeField] private Vector2 tagSize = new Vector2(124f, 42f);
        [SerializeField] private int labelFontSize = 30;

        private Button _button;
        private Image _background;
        private Text _label;
        private UiPulse _pulse;

        public event Action Clicked;

        private void Awake()
        {
            UiBuilder.EnsureEventSystem(transform);

            var canvas = UiBuilder.CreateCanvas(transform, "FeedCanvas", 10);

            _button = UiBuilder.CreateButton("FeedSlot", canvas, skin, skin != null ? skin.EmptySlot : null, new Color(0.95f, 0.75f, 0.3f, 1f));
            _background = (Image)_button.targetGraphic;
            var rect = _background.rectTransform;
            UiBuilder.Place(rect, new Vector2(0f, 1f), position, new Vector2(slotSize, slotSize));

            var food = UiBuilder.CreateImage("Food", rect, foodSprite, new Color(0.75f, 0.45f, 0.2f, 1f));
            UiBuilder.Place(food.rectTransform, new Vector2(0.5f, 0.5f), new Vector2(0f, 6f), foodSize);

            var tag = UiBuilder.CreateImage("LabelTag", rect, skin != null ? skin.PriceTag : null, new Color(0.15f, 0.15f, 0.4f, 1f));
            UiBuilder.Place(tag.rectTransform, new Vector2(0.5f, 0.5f), new Vector2(0f, tagOffsetY), tagSize);
            UiBuilder.FitSlicedBorders(tag);

            _label = UiBuilder.CreateLabel("Label", tag.rectTransform, skin, labelFontSize, TextAnchor.MiddleCenter, skin != null ? skin.TextColor : Color.white);
            UiBuilder.Stretch(_label.rectTransform, new Vector2(4f, 2f), new Vector2(-4f, 0f));

            _pulse = food.gameObject.AddComponent<UiPulse>();
            _pulse.Setup(0.12f, 2.2f);

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
            if (_background != null && skin != null)
                UiBuilder.SetSprite(_background, active ? skin.ActiveSlot : skin.EmptySlot, Color.white);

            if (_label != null)
                _label.text = active ? activeLabel : idleLabel;

            if (_pulse != null)
                _pulse.enabled = active;
        }

        private void OnClicked() => Clicked?.Invoke();
    }
}
