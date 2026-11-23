using AquariumGame.Features.Fish.Scripts.Configs.Interface;
using AquariumGame.Features.FishBehaviour.Scripts.Configs.Interface;
using AquariumGame.Features.FishVitals.Scripts.Configs.Interface;
using UnityEngine;

namespace AquariumGame.Features.Fish.Scripts.Configs
{
    [CreateAssetMenu(menuName = "Aquarium/Fish Config", fileName = "FishConfig")]
    public class FishConfig : ScriptableObject, IFishConfig, IFishVitalsConfig, IFishBehaviourConfig
    {
        [Header("Movement")]
        [SerializeField, Min(0f)] private float speed = 1f;
        [SerializeField, Min(0.001f)] private float waypointReachDistance = 0.05f;

        [Header("Vitals")]
        [SerializeField, Min(1f)] private float health = 100f;
        [SerializeField, Min(1f)] private float saturation = 100f;
        [SerializeField, Min(0f)] private float saturationTick = 2.5f;
        [SerializeField, Min(0f)] private float starvationDamage = 4f;
        [SerializeField, Min(0f)] private float regeneration = 2f;
        [SerializeField, Range(0f, 1f)] private float hungryThreshold = 0.4f;
        [SerializeField, Range(0f, 1f)] private float fullThreshold = 0.9f;

        [Header("Behaviour")]
        [SerializeField, Min(0.1f)] private float seekSpeedMultiplier = 1.6f;
        [SerializeField, Min(0f)] private float deadFloatSpeed = 0.25f;
        [SerializeField, Min(0f)] private float deadRemoveDelay = 3f;

        public float Speed => speed;

        public float WaypointReachDistance => waypointReachDistance;

        public float MaxHealth => health;

        public float MaxSaturation => saturation;

        public float SaturationDecayPerSecond => saturationTick;

        public float StarvationDamagePerSecond => starvationDamage;

        public float RegenerationPerSecond => regeneration;

        public float HungryThreshold => hungryThreshold;

        public float FullThreshold => fullThreshold;

        public float SeekSpeedMultiplier => seekSpeedMultiplier;

        public float DeadFloatSpeed => deadFloatSpeed;

        public float DeadRemoveDelay => deadRemoveDelay;
    }
}
