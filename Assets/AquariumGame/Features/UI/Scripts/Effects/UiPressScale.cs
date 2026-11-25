using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AquariumGame.Features.UI.Scripts.Effects
{
    public sealed class UiPressScale : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
    {
        [SerializeField, Range(0.5f, 1f)] private float pressedScale = 0.9f;
        [SerializeField, Min(0.01f)] private float speed = 18f;

        private Selectable _selectable;
        private float _target = 1f;
        private float _current = 1f;

        private void Awake() => _selectable = GetComponent<Selectable>();

        private void OnDisable()
        {
            _target = _current = 1f;
            transform.localScale = Vector3.one;
        }

        private void Update()
        {
            if (Mathf.Approximately(_current, _target))
                return;

            _current = Mathf.MoveTowards(_current, _target, speed * Time.unscaledDeltaTime);
            transform.localScale = new Vector3(_current, _current, 1f);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_selectable == null || _selectable.IsInteractable())
                _target = pressedScale;
        }

        public void OnPointerUp(PointerEventData eventData) => _target = 1f;

        public void OnPointerExit(PointerEventData eventData) => _target = 1f;
    }
}
