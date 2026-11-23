using AquariumGame.Features.FishTap.Scripts.Configs.Interface;
using UnityEngine;

namespace AquariumGame.Features.FishTap.Scripts.Configs
{
    [CreateAssetMenu(menuName = "Aquarium/Fish Tap Config", fileName = "FishTapConfig")]
    public sealed class FishTapConfig : ScriptableObject, IFishTapConfig
    {
        [SerializeField, Min(0)] private int coinsPerTap = 5;

        public int CoinsPerTap => coinsPerTap;
    }
}
