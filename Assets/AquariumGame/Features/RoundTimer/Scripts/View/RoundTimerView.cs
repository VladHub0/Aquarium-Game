using AquariumGame.Features.RoundTimer.Scripts.View.Interface;
using AquariumGame.Features.UI.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace AquariumGame.Features.RoundTimer.Scripts.View
{
    public sealed class RoundTimerView : MonoBehaviour, IRoundTimerView
    {
        [SerializeField] private Color panelColor = new Color(0f, 0f, 0f, 0.45f);
        [SerializeField] private Color textColor = Color.white;
        [SerializeField] private Color warningColor = new Color(1f, 0.35f, 0.3f, 1f);

        private Text _timeText;
        private int _shownSeconds = -1;

        private void Awake()
        {
            UiBuilder.EnsureEventSystem(transform);

            var canvas = UiBuilder.CreateCanvas(transform, "TimerCanvas", 10);

            var panel = UiBuilder.CreateRect("TimerPanel", canvas, typeof(Image));
            panel.anchorMin = panel.anchorMax = panel.pivot = new Vector2(0.5f, 1f);
            panel.sizeDelta = new Vector2(240f, 90f);
            panel.anchoredPosition = new Vector2(0f, -30f);

            var image = panel.GetComponent<Image>();
            image.color = panelColor;
            image.raycastTarget = false;

            _timeText = UiBuilder.CreateText("TimeText", panel, 52, TextAnchor.MiddleCenter, textColor);
            var rect = _timeText.rectTransform;
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;
        }

        public void SetTime(int seconds, bool warning)
        {
            if (_timeText == null)
                return;

            _timeText.color = warning ? warningColor : textColor;

            if (seconds == _shownSeconds)
                return;

            _shownSeconds = seconds;
            _timeText.text = $"{seconds / 60:00}:{seconds % 60:00}";
        }
    }
}
