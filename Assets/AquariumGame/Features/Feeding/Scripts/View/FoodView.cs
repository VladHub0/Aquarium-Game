using AquariumGame.Features.Feeding.Scripts.View.Interface;
using UnityEngine;

namespace AquariumGame.Features.Feeding.Scripts.View
{
    [RequireComponent(typeof(SpriteRenderer))]
    public sealed class FoodView : MonoBehaviour, IFoodView
    {
        private static Sprite _fallback;

        public Vector3 Position
        {
            get => this != null ? transform.position : Vector3.zero;
            set
            {
                if (this != null)
                    transform.position = value;
            }
        }

        public void Setup(Sprite sprite, Material material, Color color, float scale, int sortingOrder)
        {
            var spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = sprite != null ? sprite : Fallback;
            spriteRenderer.color = sprite != null ? color : new Color(0.75f, 0.45f, 0.2f, 1f);
            spriteRenderer.sortingOrder = sortingOrder;

            if (material != null)
                spriteRenderer.sharedMaterial = material;

            transform.localScale = sprite != null ? Vector3.one * scale : Vector3.one * 0.12f;
        }

        public void Destroy()
        {
            if (this != null)
                Object.Destroy(gameObject);
        }

        private static Sprite Fallback
        {
            get
            {
                if (_fallback != null)
                    return _fallback;

                var texture = Texture2D.whiteTexture;
                _fallback = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), texture.width);
                return _fallback;
            }
        }
    }
}
