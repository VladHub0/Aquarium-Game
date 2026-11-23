using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace AquariumGame.UI
{
    public static class UiBuilder
    {
        private static Font _font;

        public static Font Font => _font != null ? _font : _font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");

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

            return canvasGo.transform;
        }

        public static RectTransform CreateRect(string name, Transform parent, Type component)
        {
            var go = new GameObject(name, typeof(RectTransform), component);
            go.transform.SetParent(parent, false);
            return (RectTransform)go.transform;
        }

        public static Text CreateText(string name, Transform parent, int fontSize, TextAnchor alignment, Color color)
        {
            var rect = CreateRect(name, parent, typeof(Text));
            return SetupText(rect.GetComponent<Text>(), fontSize, alignment, color);
        }

        public static Text SetupText(Text text, int fontSize, TextAnchor alignment, Color color)
        {
            text.font = Font;
            text.fontSize = fontSize;
            text.alignment = alignment;
            text.color = color;
            text.raycastTarget = false;
            return text;
        }

        public static Image CreateIcon(Transform parent, Sprite sprite, Color fallbackColor, Vector2 anchor, Vector2 position, float size)
        {
            var rect = CreateRect("Icon", parent, typeof(Image));
            rect.anchorMin = rect.anchorMax = anchor;
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(size, size);
            rect.anchoredPosition = position;

            var image = rect.GetComponent<Image>();
            image.sprite = sprite;
            image.preserveAspect = true;
            image.raycastTarget = false;

            if (sprite == null)
                image.color = fallbackColor;

            return image;
        }

        public static Button CreateButton(Transform parent, string name)
        {
            var buttonGo = DefaultControls.CreateButton(new DefaultControls.Resources());
            buttonGo.name = name;
            buttonGo.transform.SetParent(parent, false);
            return buttonGo.GetComponent<Button>();
        }
    }
}
