using System;
using System.Collections.Generic;
using AquariumGame.Features.Shop.Scripts.View.Interface;
using AquariumGame.Features.UI.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace AquariumGame.Features.Shop.Scripts.View
{
    public sealed class ShopView : MonoBehaviour, IShopView
    {
        [SerializeField] private Sprite coinSprite;
        [SerializeField] private Color labelColor = new Color(0.15f, 0.15f, 0.2f, 1f);
        [SerializeField] private Color coinColor = new Color(1f, 0.86f, 0.25f, 1f);

        private readonly List<Button> _buttons = new List<Button>();

        private Transform _canvas;

        public event Action<int> BuyClicked;

        private void Awake()
        {
            UiBuilder.EnsureEventSystem(transform);
            _canvas = UiBuilder.CreateCanvas(transform, "ShopCanvas", 10);
        }

        private void OnDestroy()
        {
            foreach (var button in _buttons)
                if (button != null)
                    button.onClick.RemoveAllListeners();

            _buttons.Clear();
        }

        public int AddItem(string title, int price)
        {
            var index = _buttons.Count;

            var button = UiBuilder.CreateButton(_canvas, "BuyButton_" + index);
            var rect = (RectTransform)button.transform;
            rect.anchorMin = rect.anchorMax = rect.pivot = new Vector2(1f, 1f);
            rect.sizeDelta = new Vector2(420f, 80f);
            rect.anchoredPosition = new Vector2(-30f, -30f - index * 95f);

            var label = button.GetComponentInChildren<Text>();
            if (label != null)
            {
                label.rectTransform.offsetMin = new Vector2(24f, 0f);
                label.rectTransform.offsetMax = new Vector2(-24f, 0f);
                UiBuilder.SetupText(label, 32, TextAnchor.MiddleLeft, labelColor);
                label.text = title;
            }

            var priceText = UiBuilder.CreateText("Price", rect, 32, TextAnchor.MiddleRight, labelColor);
            priceText.rectTransform.anchorMin = new Vector2(0.5f, 0f);
            priceText.rectTransform.anchorMax = Vector2.one;
            priceText.rectTransform.offsetMin = Vector2.zero;
            priceText.rectTransform.offsetMax = new Vector2(-76f, 0f);
            priceText.text = price.ToString();

            UiBuilder.CreateIcon(rect, coinSprite, coinColor, new Vector2(1f, 0.5f), new Vector2(-42f, 0f), 44f);

            button.onClick.AddListener(() => BuyClicked?.Invoke(index));
            _buttons.Add(button);

            return index;
        }

        public void SetItemInteractable(int index, bool interactable)
        {
            if (index >= 0 && index < _buttons.Count)
                _buttons[index].interactable = interactable;
        }
    }
}
