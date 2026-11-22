using AquariumGame.Infrastructure.Services;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;
using Zenject;

namespace AquariumGame.UI
{
    public sealed class MainMenuView : MonoBehaviour
    {
        [SerializeField] private string playLabel = "Play";

        private IGameFlowService _gameFlow;
        private Button _playButton;

        [Inject]
        public void Construct(IGameFlowService gameFlow) => _gameFlow = gameFlow;

        private void Start()
        {
            EnsureEventSystem();
            _playButton = CreatePlayButton();
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

        private void EnsureEventSystem()
        {
            if (FindAnyObjectByType<EventSystem>() != null)
                return;

            var eventSystem = new GameObject("EventSystem", typeof(EventSystem), typeof(InputSystemUIInputModule));
            eventSystem.transform.SetParent(transform, false);
        }

        private Button CreatePlayButton()
        {
            var canvasGo = new GameObject("MainMenuCanvas", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
            canvasGo.transform.SetParent(transform, false);

            var canvas = canvasGo.GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;

            var scaler = canvasGo.GetComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1920f, 1080f);

            var buttonGo = DefaultControls.CreateButton(new DefaultControls.Resources());
            buttonGo.name = "PlayButton";
            buttonGo.transform.SetParent(canvasGo.transform, false);

            var rect = (RectTransform)buttonGo.transform;
            rect.sizeDelta = new Vector2(320f, 90f);
            rect.anchoredPosition = Vector2.zero;

            var label = buttonGo.GetComponentInChildren<Text>();
            if (label != null)
            {
                label.text = playLabel;
                label.fontSize = 36;
            }

            return buttonGo.GetComponent<Button>();
        }
    }
}
