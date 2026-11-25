using System;
using AquariumGame.Features.Hud.Scripts.View.Interface;
using AquariumGame.Features.UI.Scripts;
using AquariumGame.Features.UI.Scripts.Skin;
using UnityEngine;
using UnityEngine.UI;

namespace AquariumGame.Features.Hud.Scripts.View
{
    public sealed class HudBarView : MonoBehaviour, IHudBarView
    {
        [SerializeField] private UiSkin skin;
        [SerializeField, Min(1f)] private float barHeight = 170f;
        [SerializeField] private Color barFallbackColor = new Color(0.85f, 0.6f, 0.15f, 1f);
        [SerializeField] private float[] emptySlotsX = { 700f, 850f };
        [SerializeField] private float slotY = -70f;
        [SerializeField, Min(1f)] private float slotSize = 130f;
        [SerializeField] private float tagY = -134f;
        [SerializeField] private Vector2 tagSize = new Vector2(124f, 42f);
        [SerializeField] private string menuLabel = "MENU";
        [SerializeField] private Vector2 menuPosition = new Vector2(-172f, -44f);
        [SerializeField] private Vector2 menuSize = new Vector2(236f, 72f);

        private Button _menuButton;

        public event Action MenuClicked;

        private void Awake()
        {
            UiBuilder.EnsureEventSystem(transform);

            var canvas = UiBuilder.CreateCanvas(transform, "HudBarCanvas", 5);

            var bar = UiBuilder.CreateImage("Bar", canvas, skin != null ? skin.HudBar : null, barFallbackColor);
            bar.raycastTarget = true;
            var barRect = bar.rectTransform;
            barRect.anchorMin = new Vector2(0f, 1f);
            barRect.anchorMax = new Vector2(1f, 1f);
            barRect.pivot = new Vector2(0.5f, 1f);
            barRect.anchoredPosition = Vector2.zero;
            barRect.sizeDelta = new Vector2(0f, barHeight);

            foreach (var x in emptySlotsX)
                CreateEmptySlot(canvas, x);

            _menuButton = UiBuilder.CreateButton("MenuButton", canvas, skin, skin != null ? skin.SecondaryButton : null, new Color(1f, 0.55f, 0.1f, 1f));
            var image = (Image)_menuButton.targetGraphic;
            UiBuilder.Place(image.rectTransform, new Vector2(1f, 1f), menuPosition, menuSize);
            UiBuilder.FitSlicedBorders(image);
            UiBuilder.AddButtonLabel(_menuButton, skin, menuLabel, 40);

            _menuButton.onClick.AddListener(OnMenuClicked);
        }

        private void OnDestroy()
        {
            if (_menuButton != null)
                _menuButton.onClick.RemoveListener(OnMenuClicked);
        }

        private void CreateEmptySlot(Transform canvas, float x)
        {
            var slot = UiBuilder.CreateImage("EmptySlot", canvas, skin != null ? skin.EmptySlot : null, new Color(0.95f, 0.75f, 0.3f, 1f));
            UiBuilder.Place(slot.rectTransform, new Vector2(0f, 1f), new Vector2(x, slotY), new Vector2(slotSize, slotSize));

            var tag = UiBuilder.CreateImage("EmptyTag", canvas, skin != null ? skin.PriceTag : null, new Color(0.15f, 0.15f, 0.4f, 1f));
            UiBuilder.Place(tag.rectTransform, new Vector2(0f, 1f), new Vector2(x, tagY), tagSize);
            UiBuilder.FitSlicedBorders(tag);
        }

        private void OnMenuClicked() => MenuClicked?.Invoke();
    }
}
