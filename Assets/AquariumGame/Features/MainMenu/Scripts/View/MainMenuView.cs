using AquariumGame.Features.StateMachine.Scripts.GameFlow.Interface;
using AquariumGame.Features.UI.Scripts;
using AquariumGame.Features.UI.Scripts.Effects;
using AquariumGame.Features.UI.Scripts.Skin;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace AquariumGame.Features.MainMenu.Scripts.View
{
    public sealed class MainMenuView : MonoBehaviour
    {
        [SerializeField] private UiSkin skin;
        [SerializeField] private Sprite background;
        [SerializeField] private string title = "AQUARIUM";
        [SerializeField] private string subtitle = "Keep your fish alive!";
        [SerializeField] private string playLabel = "PLAY";
        [SerializeField] private Vector2 playSize = new Vector2(440f, 140f);

        private IGameFlowService _gameFlow;
        private Button _playButton;

        [Inject]
        public void Construct(IGameFlowService gameFlow) => _gameFlow = gameFlow;

        private void Start()
        {
            UiBuilder.EnsureEventSystem(transform);

            var canvas = UiBuilder.CreateCanvas(transform, "MainMenuCanvas", 0);

            CreateBackground(canvas);
            CreateTitle(canvas);

            var pulse = UiBuilder.CreateRect("PlayPulse", canvas, null);
            UiBuilder.Place(pulse, new Vector2(0.5f, 0.5f), new Vector2(0f, -170f), playSize);
            pulse.gameObject.AddComponent<UiPulse>().Setup(0.05f, 1.2f);

            _playButton = UiBuilder.CreateButton("PlayButton", pulse, skin, skin != null ? skin.PrimaryButton : null, new Color(0.35f, 0.8f, 0.2f, 1f));
            var image = (Image)_playButton.targetGraphic;
            UiBuilder.Place(image.rectTransform, new Vector2(0.5f, 0.5f), Vector2.zero, playSize);
            UiBuilder.FitSlicedBorders(image);
            UiBuilder.AddButtonLabel(_playButton, skin, playLabel, 76);

            _playButton.onClick.AddListener(OnPlayClicked);
        }

        private void OnDestroy()
        {
            if (_playButton != null)
                _playButton.onClick.RemoveListener(OnPlayClicked);
        }

        private void OnPlayClicked()
        {
            if (_gameFlow == null)
            {
                Debug.LogWarning("[MainMenuView] IGameFlowService не внедрён: запускайте игру со сцены Bootstrap.");
                return;
            }

            _playButton.interactable = false;
            _gameFlow.RequestStartGame();
        }

        private void CreateBackground(Transform canvas)
        {
            var image = UiBuilder.CreateImage("Background", canvas, background, new Color(0.1f, 0.55f, 0.8f, 1f));
            image.preserveAspect = false;
            UiBuilder.Stretch(image.rectTransform, Vector2.zero, Vector2.zero);

            if (background == null)
                return;

            var fitter = image.gameObject.AddComponent<AspectRatioFitter>();
            fitter.aspectMode = AspectRatioFitter.AspectMode.EnvelopeParent;
            fitter.aspectRatio = background.rect.width / background.rect.height;
        }

        private void CreateTitle(Transform canvas)
        {
            var titleText = UiBuilder.CreateLabel("Title", canvas, skin, 190, TextAnchor.MiddleCenter, skin != null ? skin.TitleColor : Color.yellow);
            UiBuilder.Place(titleText.rectTransform, new Vector2(0.5f, 0.5f), new Vector2(0f, 190f), new Vector2(1400f, 220f));
            titleText.text = title;

            foreach (var outline in titleText.GetComponents<Outline>())
                outline.effectDistance = new Vector2(7f, -7f);

            titleText.gameObject.AddComponent<UiPulse>().Setup(0.02f, 0.6f);

            var subtitleText = UiBuilder.CreateLabel("Subtitle", canvas, skin, 56, TextAnchor.MiddleCenter, skin != null ? skin.TextColor : Color.white);
            UiBuilder.Place(subtitleText.rectTransform, new Vector2(0.5f, 0.5f), new Vector2(0f, 40f), new Vector2(1400f, 80f));
            subtitleText.text = subtitle;
        }
    }
}
