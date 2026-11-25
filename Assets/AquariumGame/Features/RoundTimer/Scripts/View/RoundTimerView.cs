using AquariumGame.Features.RoundTimer.Scripts.View.Interface;
using AquariumGame.Features.UI.Scripts;
using AquariumGame.Features.UI.Scripts.Effects;
using AquariumGame.Features.UI.Scripts.Skin;
using UnityEngine;
using UnityEngine.UI;

namespace AquariumGame.Features.RoundTimer.Scripts.View
{
    public sealed class RoundTimerView : MonoBehaviour, IRoundTimerView
    {
        [SerializeField] private UiSkin skin;
        [SerializeField] private string label = "TIME";
        [SerializeField] private Vector2 position = new Vector2(1090f, -62f);
        [SerializeField] private Vector2 size = new Vector2(250f, 84f);
        [SerializeField] private float tagY = -134f;
        [SerializeField] private Vector2 tagSize = new Vector2(124f, 42f);
        [SerializeField] private int fontSize = 58;
        [SerializeField] private int labelFontSize = 30;

        private Text _timeText;
        private UiPulse _pulse;
        private int _shownSeconds = -1;

        private void Awake()
        {
            UiBuilder.EnsureEventSystem(transform);

            var canvas = UiBuilder.CreateCanvas(transform, "TimerCanvas", 10);

            var display = UiBuilder.CreateImage("TimerDisplay", canvas, skin != null ? skin.TimerDisplay : null, new Color(0.05f, 0.1f, 0.3f, 1f));
            UiBuilder.Place(display.rectTransform, new Vector2(0f, 1f), position, size);
            UiBuilder.FitSlicedBorders(display);

            _timeText = UiBuilder.CreateLabel("TimeText", display.rectTransform, skin, fontSize, TextAnchor.MiddleCenter, TextColor);
            UiBuilder.Stretch(_timeText.rectTransform, new Vector2(0f, 2f), Vector2.zero);

            _pulse = display.gameObject.AddComponent<UiPulse>();
            _pulse.Setup(0.07f, 2f);
            _pulse.enabled = false;

            var tag = UiBuilder.CreateImage("LabelTag", canvas, skin != null ? skin.PriceTag : null, new Color(0.15f, 0.15f, 0.4f, 1f));
            UiBuilder.Place(tag.rectTransform, new Vector2(0f, 1f), new Vector2(position.x, tagY), tagSize);
            UiBuilder.FitSlicedBorders(tag);

            var tagText = UiBuilder.CreateLabel("Label", tag.rectTransform, skin, labelFontSize, TextAnchor.MiddleCenter, skin != null ? skin.TextColor : Color.white);
            UiBuilder.Stretch(tagText.rectTransform, new Vector2(4f, 2f), new Vector2(-4f, 0f));
            tagText.text = label;
        }

        private Color TextColor => skin != null ? skin.TimerTextColor : Color.white;

        private Color WarningColor => skin != null ? skin.WarningTextColor : new Color(1f, 0.35f, 0.3f, 1f);

        public void SetTime(int seconds, bool warning)
        {
            if (_timeText == null)
                return;

            _timeText.color = warning ? WarningColor : TextColor;

            if (_pulse != null && _pulse.enabled != warning)
                _pulse.enabled = warning;

            if (seconds == _shownSeconds)
                return;

            _shownSeconds = seconds;
            _timeText.text = $"{seconds / 60:00}:{seconds % 60:00}";
        }
    }
}
