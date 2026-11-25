using System;
using AquariumGame.Features.UI.Scripts.Effects;
using AquariumGame.Features.UI.Scripts.Skin;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace AquariumGame.Features.UI.Scripts
{
    public static class UiBuilder
    {
        private static Font _defaultFont;

        public static Font DefaultFont => _defaultFont != null ? _defaultFont : _defaultFont = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");

        public static void EnsureEventSystem(Transform owner)
        {
            if (Object.FindAnyObjectByType<EventSystem>() != null)
                return;

            var eventSystem = new GameObject("EventSystem", typeof(EventSystem), typeof(InputSystemUIInputModule));
            eventSystem.transform.SetParent(owner, false);
        }

        public static Transform CreateCanvas(Transform parent, string name, int sortingOrder)
        {
            var canvasGo = new GameObject(name, typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
            canvasGo.transform.SetParent(parent, false);

            var canvas = canvasGo.GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.sortingOrder = sortingOrder;

            var scaler = canvasGo.GetComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1920f, 1080f);
            scaler.matchWidthOrHeight = 0.5f;

            return canvasGo.transform;
        }

        public static RectTransform CreateRect(string name, Transform parent, Type component)
        {
            var go = component != null
                ? new GameObject(name, typeof(RectTransform), component)
                : new GameObject(name, typeof(RectTransform));

            go.transform.SetParent(parent, false);
            return (RectTransform)go.transform;
        }

        public static RectTransform Place(RectTransform rect, Vector2 anchor, Vector2 position, Vector2 size)
        {
            rect.anchorMin = rect.anchorMax = anchor;
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.anchoredPosition = position;
            rect.sizeDelta = size;
            return rect;
        }

        public static RectTransform Stretch(RectTransform rect, Vector2 offsetMin, Vector2 offsetMax)
        {
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.offsetMin = offsetMin;
            rect.offsetMax = offsetMax;
            return rect;
        }

        public static Image CreateImage(string name, Transform parent, Sprite sprite, Color fallbackColor)
        {
            var image = CreateRect(name, parent, typeof(Image)).GetComponent<Image>();
            image.raycastTarget = false;
            SetSprite(image, sprite, fallbackColor);
            return image;
        }

        public static void SetSprite(Image image, Sprite sprite, Color fallbackColor)
        {
            image.sprite = sprite;
            image.color = sprite != null ? Color.white : fallbackColor;

            if (sprite != null && sprite.border != Vector4.zero)
            {
                image.type = Image.Type.Sliced;
                image.fillCenter = true;
            }
            else
            {
                image.type = Image.Type.Simple;
                image.preserveAspect = sprite != null;
            }
        }

        public static void FitSlicedBorders(Image image)
        {
            if (image.sprite == null || image.type != Image.Type.Sliced)
                return;

            var rect = image.rectTransform.rect;
            var border = image.sprite.border;
            var needX = (border.x + border.z) / Mathf.Max(1f, rect.width);
            var needY = (border.y + border.w) / Mathf.Max(1f, rect.height);
            image.pixelsPerUnitMultiplier = Mathf.Max(1f, needX, needY);
        }

        public static Text CreateLabel(string name, Transform parent, UiSkin skin, int fontSize, TextAnchor alignment, Color color)
        {
            var text = CreateRect(name, parent, typeof(Text)).GetComponent<Text>();
            text.font = skin != null && skin.Font != null ? skin.Font : DefaultFont;
            text.fontSize = fontSize;
            text.alignment = alignment;
            text.color = color;
            text.raycastTarget = false;
            text.horizontalOverflow = HorizontalWrapMode.Overflow;
            text.verticalOverflow = VerticalWrapMode.Overflow;

            if (skin != null)
            {
                var shadow = text.gameObject.AddComponent<Shadow>();
                shadow.effectColor = skin.ShadowColor;
                shadow.effectDistance = skin.ShadowDistance;

                var outline = text.gameObject.AddComponent<Outline>();
                outline.effectColor = skin.OutlineColor;
                outline.effectDistance = skin.OutlineDistance;
            }

            return text;
        }

        public static Button CreateButton(string name, Transform parent, UiSkin skin, Sprite sprite, Color fallbackColor)
        {
            var rect = CreateRect(name, parent, typeof(Image));
            var image = rect.GetComponent<Image>();
            SetSprite(image, sprite, fallbackColor);
            image.raycastTarget = true;

            var button = rect.gameObject.AddComponent<Button>();
            button.targetGraphic = image;
            button.transition = Selectable.Transition.ColorTint;

            var colors = button.colors;
            colors.normalColor = Color.white;
            colors.highlightedColor = new Color(1f, 1f, 0.92f, 1f);
            colors.pressedColor = new Color(0.85f, 0.85f, 0.85f, 1f);
            colors.selectedColor = Color.white;
            colors.disabledColor = skin != null ? skin.DisabledTint : new Color(0.6f, 0.6f, 0.6f, 1f);
            colors.fadeDuration = 0.08f;
            button.colors = colors;

            rect.gameObject.AddComponent<UiPressScale>();
            return button;
        }

        public static Text AddButtonLabel(Button button, UiSkin skin, string label, int fontSize)
        {
            var text = CreateLabel("Label", button.transform, skin, fontSize, TextAnchor.MiddleCenter, skin != null ? skin.TextColor : Color.white);
            Stretch(text.rectTransform, new Vector2(12f, 6f), new Vector2(-12f, 0f));
            text.text = label;
            return text;
        }
    }
}
