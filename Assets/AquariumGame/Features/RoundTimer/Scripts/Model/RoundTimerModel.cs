using System;
using AquariumGame.Features.RoundTimer.Scripts.Configs.Interface;
using AquariumGame.Features.RoundTimer.Scripts.Model.Interface;
using UnityEngine;

namespace AquariumGame.Features.RoundTimer.Scripts.Model
{
    public sealed class RoundTimerModel : IRoundTimerModel
    {
        public RoundTimerModel(IRoundTimerConfig config) => Remaining = config.Duration;

        public float Remaining { get; private set; }

        public bool IsFinished => Remaining <= 0f;

        public event Action<float> Changed;

        public void Reset(float duration)
        {
            Remaining = Mathf.Max(0f, duration);
            Changed?.Invoke(Remaining);
        }

        public void Tick(float deltaTime)
        {
            if (IsFinished || deltaTime <= 0f)
                return;

            Remaining = Mathf.Max(0f, Remaining - deltaTime);
            Changed?.Invoke(Remaining);
        }
    }
}
