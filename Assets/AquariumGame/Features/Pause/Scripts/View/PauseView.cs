using AquariumGame.Features.StateMachine.Scripts.GameFlow.Interface;
using AquariumGame.Features.UI.Scripts;
using AquariumGame.Features.UI.Scripts.Effects;
using AquariumGame.Features.UI.Scripts.Skin;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace AquariumGame.Features.Pause.Scripts.View
{
    public sealed class PauseView : MonoBehaviour
    {
        [SerializeField] private UiSkin skin;
        [SerializeField] private string titleLabel = "PAUSED";
        [SerializeField] private string resumeLabel = "RESUME";
        [SerializeField] private string mainMenuLabel = "MAIN MENU";
        [SerializeField] private Vector2 panelSize = new Vector2(640f, 600f);
        [SerializeField] private Vector2 buttonSize = new Vector2(420f, 120f);

        private IGameFlowService _gameFlow;
        private Button _resumeButton;
        private Button _mainMenuButton;

        [Inject]
        public void Construct(IGameFlowService gameFlow) => _gameFlow = gameFlow;

        private void Start()
        {
            UiBuilder.EnsureEventSystem(transform);

            var canvas = UiBuilder.CreateCanvas(transform, "PauseCanvas", 100);

            var dim = UiBuilder.CreateImage("Dim", canvas, null, skin != null ? skin.DimColor : new Color(0f, 0f, 0f, 0.6f));
            dim.raycastTarget = true;
            UiBuilder.Stretch(dim.rectTransform, Vector2.zero, Vector2.zero);

            var panel = UiBuilder.CreateImage("Panel", canvas, skin != null ? skin.Panel : null, new Color(0.7f, 0.35f, 0.85f, 1f));
            panel.raycastTarget = true;
            UiBuilder.Place(panel.rectTransform, new Vector2(0.5f, 0.5f), Vector2.zero, panelSize);
            UiBuilder.FitSlicedBorders(panel);

            var title = UiBuilder.CreateLabel("Title", panel.rectTransform, skin, 104, TextAnchor.MiddleCenter, skin != null ? skin.TitleColor : Color.yellow);
            UiBuilder.Place(title.rectTransform, new Vector2(0.5f, 1f), new Vector2(0f, -110f), new Vector2(panelSize.x, 140f));
            title.text = titleLabel;

            foreach (var outline in title.GetComponents<Outline>())
                outline.effectDistance = new Vector2(5f, -5f);

            _resumeButton = CreateButton(panel.rectTransform, "ResumeButton", skin != null ? skin.PrimaryButton : null, resumeLabel, new Vector2(0f, -10f));
            _mainMenuButton = CreateButton(panel.rectTransform, "MainMenuButton", skin != null ? skin.SecondaryButton : null, mainMenuLabel, new Vector2(0f, -170f));

            _resumeButton.onClick.AddListener(OnResumeClicked);
            _mainMenuButton.onClick.AddListener(OnMainMenuClicked);

            panel.gameObject.AddComponent<UiPunch>().Play();
        }

        private void OnDestroy()
        {
            if (_resumeButton != null)
                _resumeButton.onClick.RemoveListener(OnResumeClicked);

            if (_mainMenuButton != null)
                _mainMenuButton.onClick.RemoveListener(OnMainMenuClicked);
        }

        private void OnResumeClicked()
        {
            if (!CanUseGameFlow())
                return;

            SetInteractable(false);
            _gameFlow.RequestTogglePause();
        }

        private void OnMainMenuClicked()
        {
            if (!CanUseGameFlow())
                return;

            SetInteractable(false);
            _gameFlow.RequestMainMenu();
        }

        private bool CanUseGameFlow()
        {
            if (_gameFlow != null)
                return true;

            Debug.LogWarning("[PauseView] IGameFlowService не внедрён: запускайте игру со сцены Bootstrap.");
            return false;
        }

        private void SetInteractable(bool value)
        {
            _resumeButton.interactable = value;
            _mainMenuButton.interactable = value;
        }

        private Button CreateButton(Transform parent, string name, Sprite sprite, string label, Vector2 position)
        {
            var button = UiBuilder.CreateButton(name, parent, skin, sprite, new Color(0.35f, 0.8f, 0.2f, 1f));
            var image = (Image)button.targetGraphic;
            UiBuilder.Place(image.rectTransform, new Vector2(0.5f, 0.5f), position, buttonSize);
            UiBuilder.FitSlicedBorders(image);
            UiBuilder.AddButtonLabel(button, skin, label, 58);
            return button;
        }
    }
}
