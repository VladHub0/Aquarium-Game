using AquariumGame.Features.RoundTimer.Scripts.Configs.Interface;
using UnityEngine;

namespace AquariumGame.Features.RoundTimer.Scripts.Configs
{
    [CreateAssetMenu(menuName = "Aquarium/Round Timer Config", fileName = "RoundTimerConfig")]
    public sealed class RoundTimerConfig : ScriptableObject, IRoundTimerConfig
    {
        [SerializeField, Min(1f)] private float duration = 120f;
        [SerializeField, Min(0f)] private float warningTime = 10f;

        public float Duration => duration;

        public float WarningTime => warningTime;
    }
}
