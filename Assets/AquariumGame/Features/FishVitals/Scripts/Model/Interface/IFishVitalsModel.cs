using System;

namespace AquariumGame.Features.FishVitals.Scripts.Model.Interface
{
    public interface IFishVitalsModel
    {
        float MaxHealth { get; }

        float Health { get; }

        float MaxSaturation { get; }

        float Saturation { get; }

        float HealthNormalized { get; }

        float SaturationNormalized { get; }

        bool IsDead { get; }

        event Action Changed;

        event Action Died;

        void ChangeHealth(float delta);

        void ChangeSaturation(float delta);
    }
}
