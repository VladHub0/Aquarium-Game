using AquariumGame.Features.Fish.Scripts.Configs.Interface;
using UnityEngine;


namespace AquariumGame.Features.Fish.Scripts.Configs
{
    [CreateAssetMenu(menuName = "Aquarium/Fish Config", fileName = "FishConfig")]
    public class FishConfig : ScriptableObject, IFishConfig
    {

        [Header("Movement")]
        [SerializeField, Min(0f)] private float speed = 1f;
        [SerializeField, Min(0.001f)] private float waypointReachDistance = 0.05f;

        [Header("Health")]
        [SerializeField, Min(0f)] private float health = 100f;
        [SerializeField, Min(0f)] private float saturation = 100f;
        [SerializeField, Min(0f)] private float saturationTick = 0.05f;


        public float Speed { get => speed; }
        public float WaypointReachDistance { get => waypointReachDistance; }
        public float Health { get => health; }

        public float Saturation { get => saturation; }

        public float SaturationTick { get => saturationTick; }
    }
}
