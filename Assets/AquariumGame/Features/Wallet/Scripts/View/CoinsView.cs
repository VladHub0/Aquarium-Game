using AquariumGame.Features.UI.Scripts;
using AquariumGame.Features.UI.Scripts.Effects;
using AquariumGame.Features.UI.Scripts.Skin;
using AquariumGame.Features.Wallet.Scripts.View.Interface;
using UnityEngine;
using UnityEngine.UI;

namespace AquariumGame.Features.Wallet.Scripts.View
{
    public sealed class CoinsView : MonoBehaviour, ICoinsView
    {
        [SerializeField] private UiSkin skin;
        [SerializeField] private Vector2 position = new Vector2(-172f, -116f);
        [SerializeField] private Vector2 size = new Vector2(310f, 76f);
        [SerializeField, Min(1f)] private float coinSize = 54f;
        [SerializeField] private int fontSize = 50;

        private Text _coinsText;
        private UiPunch _punch;
        private int _shownCoins = -1;

        private void Awake()
        {
            UiBuilder.EnsureEventSystem(transform);

            var canvas = UiBuilder.CreateCanvas(transform, "CoinsCanvas", 10);

            var display = UiBuilder.CreateImage("CoinsDisplay", canvas, skin != null ? skin.CoinsDisplay : null, new Color(0.05f, 0.25f, 0.08f, 1f));
            UiBuilder.Place(display.rectTransform, new Vector2(1f, 1f), position, size);
            UiBuilder.FitSlicedBorders(display);
            _punch = display.gameObject.AddComponent<UiPunch>();

            var coin = UiBuilder.CreateImage("Coin", display.rectTransform, skin != null ? skin.Coin : null, new Color(1f, 0.85f, 0.2f, 1f));
            UiBuilder.Place(coin.rectTransform, new Vector2(0f, 0.5f), new Vector2(coinSize * 0.5f + 16f, 0f), new Vector2(coinSize, coinSize));

            var color = skin != null ? skin.CoinsTextColor : new Color(0.55f, 1f, 0.35f, 1f);
            _coinsText = UiBuilder.CreateLabel("CoinsText", display.rectTransform, skin, fontSize, TextAnchor.MiddleRight, color);
            UiBuilder.Stretch(_coinsText.rectTransform, new Vector2(coinSize + 24f, 0f), new Vector2(-24f, 0f));
        }

        public void SetCoins(int coins)
        {
            if (_coinsText == null)
                return;

            if (_shownCoins >= 0 && coins != _shownCoins && _punch != null)
                _punch.Play();

            _shownCoins = coins;
            _coinsText.text = coins.ToString();
        }
    }
}
