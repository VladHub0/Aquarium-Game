using AquariumGame.Features.Feeding.Scripts.Configs.Interface;
using UnityEngine;

namespace AquariumGame.Features.Feeding.Scripts.Configs
{
    [CreateAssetMenu(menuName = "Aquarium/Feeding Config", fileName = "FeedingConfig")]
    public sealed class FeedingConfig : ScriptableObject, IFeedingConfig
    {
        [Header("View")]
        [SerializeField] private Sprite foodSprite;
        [SerializeField] private Material foodMaterial;
        [SerializeField] private Color foodColor = Color.white;
        [SerializeField, Min(0.01f)] private float foodScale = 1f;
        [SerializeField] private int sortingOrder = 15;

        [Header("Spawn")]
        [SerializeField, Min(1)] private int pelletsPerTap = 3;
        [SerializeField, Min(0f)] private float scatterRadius = 0.3f;

        [Header("Food")]
        [SerializeField, Min(0f)] private float nutrition = 35f;
        [SerializeField, Min(0f)] private float sinkSpeed = 0.3f;
        [SerializeField] private float bottomY = -2.2f;
        [SerializeField, Min(0.1f)] private float lifetime = 25f;
        [SerializeField, Min(0.01f)] private float eatDistance = 0.2f;

        public Sprite FoodSprite => foodSprite;

        public Material FoodMaterial => foodMaterial;

        public Color FoodColor => foodColor;

        public float FoodScale => foodScale;

        public int SortingOrder => sortingOrder;

        public int PelletsPerTap => pelletsPerTap;

        public float ScatterRadius => scatterRadius;

        public float Nutrition => nutrition;

        public float SinkSpeed => sinkSpeed;

        public float BottomY => bottomY;

        public float Lifetime => lifetime;

        public float EatDistance => eatDistance;
    }
}
