using System;

namespace AquariumGame.Features.RoundTimer.Scripts.Model.Interface
{
    public interface IRoundTimerModel
    {
        float Remaining { get; }

        bool IsFinished { get; }

        event Action<float> Changed;

        void Reset(float duration);

        void Tick(float deltaTime);
    }
}
