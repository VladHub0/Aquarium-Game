using System;
using System.Collections.Generic;
using AquariumGame.Infrastructure.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;
using Zenject;

namespace AquariumGame.UI
{
    public sealed class HudView : MonoBehaviour
    {
        [SerializeField] private Sprite coinSprite;
        [SerializeField] private Color panelColor = new Color(0f, 0f, 0f, 0.45f);
        [SerializeField] private Color coinTextColor = new Color(1f, 0.86f, 0.25f, 1f);

        private readonly List<ShopButton> _shopButtons = new List<ShopButton>();

        private IWalletService _wallet;
        private IFishShop _shop;
        private Font _font;
        private Text _coinsText;

        [Inject]
        public void Construct([InjectOptional] IWalletService wallet, [InjectOptional] IFishShop shop)
        {
            _wallet = wallet;
            _shop = shop;
        }

        private void Start()
        {
            _font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");

            EnsureEventSystem();

            var canvas = CreateCanvas();
            CreateCoinsPanel(canvas);
            CreateShop(canvas);

            if (_wallet != null)
                _wallet.Changed += OnCoinsChanged;

            OnCoinsChanged(_wallet?.Coins ?? 0);
        }

        private void Update()
        {
            foreach (var shopButton in _shopButtons)
                shopButton.Button.interactable = _shop != null && _shop.CanBuy(shopButton.Species);
        }

        private void OnDestroy()
        {
            if (_wallet != null)
                _wallet.Changed -= OnCoinsChanged;

            foreach (var shopButton in _shopButtons)
                if (shopButton.Button != null)
                    shopButton.Button.onClick.RemoveAllListeners();

            _shopButtons.Clear();
        }

        private void OnCoinsChanged(int coins)
        {
            if (_coinsText != null)
                _coinsText.text = coins.ToString();
        }

        private async UniTaskVoid BuyAsync(FishSpecies species)
        {
            if (_shop == null)
            {
                Debug.LogWarning("[HudView] IFishShop не внедрён: запускайте игру со сцены Bootstrap.");
                return;
            }

            try
            {
                await _shop.TryBuyAsync(species, this.GetCancellationTokenOnDestroy());
            }
            catch (OperationCanceledException)
            {
            }
        }

        private void EnsureEventSystem()
        {
            if (FindAnyObjectByType<EventSystem>() != null)
                return;

            var eventSystem = new GameObject("EventSystem", typeof(EventSystem), typeof(InputSystemUIInputModule));
            eventSystem.transform.SetParent(transform, false);
        }

        private Transform CreateCanvas()
        {
            var canvasGo = new GameObject("HudCanvas", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
            canvasGo.transform.SetParent(transform, false);

            var canvas = canvasGo.GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.sortingOrder = 10;

            var scaler = canvasGo.GetComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1920f, 1080f);

            return canvasGo.transform;
        }

        private void CreateCoinsPanel(Transform canvas)
        {
            var panel = CreateRect("CoinsPanel", canvas, typeof(Image));
            panel.anchorMin = panel.anchorMax = panel.pivot = new Vector2(0f, 1f);
            panel.sizeDelta = new Vector2(300f, 90f);
            panel.anchoredPosition = new Vector2(30f, -30f);
            panel.GetComponent<Image>().color = panelColor;

            CreateCoinIcon(panel, new Vector2(0f, 0.5f), new Vector2(50f, 0f), 64f);

            var label = CreateRect("CoinsText", panel, typeof(Text));
            label.anchorMin = Vector2.zero;
            label.anchorMax = Vector2.one;
            label.offsetMin = new Vector2(100f, 0f);
            label.offsetMax = new Vector2(-20f, 0f);

            _coinsText = SetupText(label.GetComponent<Text>(), 52, TextAnchor.MiddleLeft, coinTextColor);
        }

        private void CreateShop(Transform canvas)
        {
            var species = FishCatalog.All;
            for (var i = 0; i < species.Length; i++)
                _shopButtons.Add(CreateShopButton(canvas, species[i], i));
        }

        private ShopButton CreateShopButton(Transform canvas, FishSpecies species, int index)
        {
            var buttonGo = DefaultControls.CreateButton(new DefaultControls.Resources());
            buttonGo.name = species.Key + "BuyButton";
            buttonGo.transform.SetParent(canvas, false);

            var rect = (RectTransform)buttonGo.transform;
            rect.anchorMin = rect.anchorMax = rect.pivot = new Vector2(1f, 1f);
            rect.sizeDelta = new Vector2(420f, 80f);
            rect.anchoredPosition = new Vector2(-30f, -30f - index * 95f);

            var text = buttonGo.GetComponentInChildren<Text>();
            if (text != null)
            {
                var textRect = (RectTransform)text.transform;
                textRect.offsetMin = new Vector2(24f, 0f);
                textRect.offsetMax = new Vector2(-24f, 0f);
                SetupText(text, 32, TextAnchor.MiddleLeft, new Color(0.15f, 0.15f, 0.2f, 1f));
                text.text = species.DisplayName;
            }

            var price = CreateRect("Price", rect, typeof(Text));
            price.anchorMin = new Vector2(0.5f, 0f);
            price.anchorMax = Vector2.one;
            price.offsetMin = Vector2.zero;
            price.offsetMax = new Vector2(-76f, 0f);
            SetupText(price.GetComponent<Text>(), 32, TextAnchor.MiddleRight, new Color(0.15f, 0.15f, 0.2f, 1f)).text = species.Price.ToString();

            CreateCoinIcon(rect, new Vector2(1f, 0.5f), new Vector2(-42f, 0f), 44f);

            var button = buttonGo.GetComponent<Button>();
            button.onClick.AddListener(() => BuyAsync(species).Forget());

            return new ShopButton(species, button);
        }

        private void CreateCoinIcon(Transform parent, Vector2 anchor, Vector2 position, float size)
        {
            var icon = CreateRect("CoinIcon", parent, typeof(Image));
            icon.anchorMin = icon.anchorMax = anchor;
            icon.pivot = new Vector2(0.5f, 0.5f);
            icon.sizeDelta = new Vector2(size, size);
            icon.anchoredPosition = position;

            var image = icon.GetComponent<Image>();
            image.sprite = coinSprite;
            image.preserveAspect = true;
            image.raycastTarget = false;

            if (coinSprite == null)
                image.color = coinTextColor;
        }

        private Text SetupText(Text text, int fontSize, TextAnchor alignment, Color color)
        {
            text.font = _font;
            text.fontSize = fontSize;
            text.alignment = alignment;
            text.color = color;
            text.raycastTarget = false;
            return text;
        }

        private static RectTransform CreateRect(string name, Transform parent, Type component)
        {
            var go = new GameObject(name, typeof(RectTransform), component);
            go.transform.SetParent(parent, false);
            return (RectTransform)go.transform;
        }

        private readonly struct ShopButton
        {
            public readonly FishSpecies Species;
            public readonly Button Button;

            public ShopButton(FishSpecies species, Button button)
            {
                Species = species;
                Button = button;
            }
        }
    }
}
