using AquariumGame.Infrastructure.Services;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;
using Zenject;

namespace AquariumGame.UI
{
    public sealed class PauseView : MonoBehaviour
    {
        [SerializeField] private string titleLabel = "Paused";
        [SerializeField] private string resumeLabel = "Resume";
        [SerializeField] private string mainMenuLabel = "Main Menu";
        [SerializeField] private Color dimColor = new Color(0f, 0f, 0f, 0.6f);

        private IGameFlowService _gameFlow;
        private Button _resumeButton;
        private Button _mainMenuButton;

        [Inject]
        public void Construct(IGameFlowService gameFlow) => _gameFlow = gameFlow;

        private void Start()
        {
            EnsureEventSystem();

            var canvas = CreateCanvas();
            CreateDim(canvas);
            CreateTitle(canvas);

            _resumeButton = CreateButton(canvas, "ResumeButton", resumeLabel, new Vector2(0f, 0f));
            _mainMenuButton = CreateButton(canvas, "MainMenuButton", mainMenuLabel, new Vector2(0f, -120f));

            _resumeButton.onClick.AddListener(OnResumeClicked);
            _mainMenuButton.onClick.AddListener(OnMainMenuClicked);
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

        private void EnsureEventSystem()
        {
            if (FindAnyObjectByType<EventSystem>() != null)
                return;

            var eventSystem = new GameObject("EventSystem", typeof(EventSystem), typeof(InputSystemUIInputModule));
            eventSystem.transform.SetParent(transform, false);
        }

        private Transform CreateCanvas()
        {
            var canvasGo = new GameObject("PauseCanvas", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
            canvasGo.transform.SetParent(transform, false);

            var canvas = canvasGo.GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.sortingOrder = 100;

            var scaler = canvasGo.GetComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1920f, 1080f);

            return canvasGo.transform;
        }

        private void CreateDim(Transform canvas)
        {
            var dimGo = new GameObject("Dim", typeof(RectTransform), typeof(Image));
            dimGo.transform.SetParent(canvas, false);

            var rect = (RectTransform)dimGo.transform;
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;

            dimGo.GetComponent<Image>().color = dimColor;
        }

        private void CreateTitle(Transform canvas)
        {
            var titleGo = new GameObject("Title", typeof(RectTransform), typeof(Text));
            titleGo.transform.SetParent(canvas, false);

            var rect = (RectTransform)titleGo.transform;
            rect.sizeDelta = new Vector2(800f, 140f);
            rect.anchoredPosition = new Vector2(0f, 180f);

            var text = titleGo.GetComponent<Text>();
            text.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            text.text = titleLabel;
            text.fontSize = 96;
            text.alignment = TextAnchor.MiddleCenter;
            text.color = Color.white;
        }

        private static Button CreateButton(Transform canvas, string name, string label, Vector2 position)
        {
            var buttonGo = DefaultControls.CreateButton(new DefaultControls.Resources());
            buttonGo.name = name;
            buttonGo.transform.SetParent(canvas, false);

            var rect = (RectTransform)buttonGo.transform;
            rect.sizeDelta = new Vector2(360f, 90f);
            rect.anchoredPosition = position;

            var text = buttonGo.GetComponentInChildren<Text>();
            if (text != null)
            {
                text.text = label;
                text.fontSize = 36;
            }

            return buttonGo.GetComponent<Button>();
        }
    }
}
