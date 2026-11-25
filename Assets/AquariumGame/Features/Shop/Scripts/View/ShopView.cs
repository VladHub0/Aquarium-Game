using System;
using System.Collections.Generic;
using AquariumGame.Features.Shop.Scripts.View.Interface;
using AquariumGame.Features.UI.Scripts;
using AquariumGame.Features.UI.Scripts.Skin;
using UnityEngine;
using UnityEngine.UI;

namespace AquariumGame.Features.Shop.Scripts.View
{
    public sealed class ShopView : MonoBehaviour, IShopView
    {
        [SerializeField] private UiSkin skin;
        [SerializeField] private ShopItemIcon[] icons = Array.Empty<ShopItemIcon>();
        [SerializeField] private float firstSlotX = 250f;
        [SerializeField] private float slotSpacing = 150f;
        [SerializeField] private float slotY = -70f;
        [SerializeField, Min(1f)] private float slotSize = 130f;
        [SerializeField] private Vector2 iconSize = new Vector2(104f, 76f);
        [SerializeField] private float tagOffsetY = -64f;
        [SerializeField] private Vector2 tagSize = new Vector2(124f, 42f);
        [SerializeField, Min(1f)] private float coinSize = 30f;
        [SerializeField] private int priceFontSize = 30;

        private readonly List<Item> _items = new List<Item>();

        private Transform _canvas;

        public event Action<int> BuyClicked;

        private void Awake()
        {
            UiBuilder.EnsureEventSystem(transform);
            _canvas = UiBuilder.CreateCanvas(transform, "ShopCanvas", 10);
        }

        private void OnDestroy()
        {
            foreach (var item in _items)
                if (item.Button != null)
                    item.Button.onClick.RemoveAllListeners();

            _items.Clear();
        }

        public int AddItem(string key, int price)
        {
            var index = _items.Count;
            var x = firstSlotX + index * slotSpacing;

            var button = UiBuilder.CreateButton("ShopSlot_" + index, _canvas, skin, skin != null ? skin.Slot : null, new Color(0.2f, 0.5f, 1f, 1f));
            var rect = (RectTransform)button.transform;
            UiBuilder.Place(rect, new Vector2(0f, 1f), new Vector2(x, slotY), new Vector2(slotSize, slotSize));

            var icon = UiBuilder.CreateImage("Icon", rect, FindIcon(key), new Color(1f, 0.6f, 0.2f, 1f));
            UiBuilder.Place(icon.rectTransform, new Vector2(0.5f, 0.5f), new Vector2(0f, 4f), iconSize);

            var tag = UiBuilder.CreateImage("PriceTag", rect, skin != null ? skin.PriceTag : null, new Color(0.15f, 0.15f, 0.4f, 1f));
            UiBuilder.Place(tag.rectTransform, new Vector2(0.5f, 0.5f), new Vector2(0f, tagOffsetY), tagSize);
            UiBuilder.FitSlicedBorders(tag);

            var coin = UiBuilder.CreateImage("Coin", tag.rectTransform, skin != null ? skin.Coin : null, new Color(1f, 0.85f, 0.2f, 1f));
            UiBuilder.Place(coin.rectTransform, new Vector2(0f, 0.5f), new Vector2(coinSize * 0.5f + 10f, 0f), new Vector2(coinSize, coinSize));

            var priceText = UiBuilder.CreateLabel("Price", tag.rectTransform, skin, priceFontSize, TextAnchor.MiddleCenter, skin != null ? skin.TextColor : Color.white);
            UiBuilder.Stretch(priceText.rectTransform, new Vector2(coinSize + 10f, 2f), new Vector2(-8f, 0f));
            priceText.text = price.ToString();

            button.onClick.AddListener(() => BuyClicked?.Invoke(index));
            _items.Add(new Item(button, icon, priceText));

            return index;
        }

        public void SetItemInteractable(int index, bool interactable)
        {
            if (index < 0 || index >= _items.Count)
                return;

            var item = _items[index];
            item.Button.interactable = interactable;

            var tint = interactable ? Color.white : skin != null ? skin.DisabledTint : Color.gray;

            if (item.Icon.sprite != null)
                item.Icon.color = tint;

            item.Price.color = interactable ? skin != null ? skin.TextColor : Color.white : tint;
        }

        private Sprite FindIcon(string key)
        {
            foreach (var entry in icons)
                if (entry.Key == key)
                    return entry.Icon;

            return null;
        }

        private readonly struct Item
        {
            public readonly Button Button;
            public readonly Image Icon;
            public readonly Text Price;

            public Item(Button button, Image icon, Text price)
            {
                Button = button;
                Icon = icon;
                Price = price;
            }
        }
    }
}
