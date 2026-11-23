using AquariumGame.Features.UI.Scripts;
using AquariumGame.Features.Wallet.Scripts.View.Interface;
using UnityEngine;
using UnityEngine.UI;

namespace AquariumGame.Features.Wallet.Scripts.View
{
    public sealed class CoinsView : MonoBehaviour, ICoinsView
    {
        [SerializeField] private Sprite coinSprite;
        [SerializeField] private Color panelColor = new Color(0f, 0f, 0f, 0.45f);
        [SerializeField] private Color textColor = new Color(1f, 0.86f, 0.25f, 1f);

        private Text _coinsText;

        private void Awake()
        {
            UiBuilder.EnsureEventSystem(transform);

            var canvas = UiBuilder.CreateCanvas(transform, "CoinsCanvas", 10);

            var panel = UiBuilder.CreateRect("CoinsPanel", canvas, typeof(Image));
            panel.anchorMin = panel.anchorMax = panel.pivot = new Vector2(0f, 1f);
            panel.sizeDelta = new Vector2(300f, 90f);
            panel.anchoredPosition = new Vector2(30f, -30f);
            panel.GetComponent<Image>().color = panelColor;

            UiBuilder.CreateIcon(panel, coinSprite, textColor, new Vector2(0f, 0.5f), new Vector2(50f, 0f), 64f);

            _coinsText = UiBuilder.CreateText("CoinsText", panel, 52, TextAnchor.MiddleLeft, textColor);
            var rect = _coinsText.rectTransform;
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMin = new Vector2(100f, 0f);
            rect.offsetMax = new Vector2(-20f, 0f);
        }

        public void SetCoins(int coins)
        {
            if (_coinsText != null)
                _coinsText.text = coins.ToString();
        }
    }
}
