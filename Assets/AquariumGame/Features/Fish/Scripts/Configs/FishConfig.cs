using AquariumGame.Features.Fish.Scripts.Configs.Interface;
using UnityEngine;


namespace AquariumGame.Features.Fish.Scripts.Configs
{
    [CreateAssetMenu(menuName = "Aquarium/Fish Config", fileName = "FishConfig")]
    public class FishConfig : ScriptableObject, IFishConfig
    {

        [Header("Movement")]
        [SerializeField, Min(0f)] private float speed = 1f;
        public float Speed { get => speed; }
    }
}
