using System;
using AquariumGame.Features.FishVitals.Scripts.Configs.Interface;
using AquariumGame.Features.FishVitals.Scripts.Model.Interface;
using UnityEngine;

namespace AquariumGame.Features.FishVitals.Scripts.Model
{
    public sealed class FishVitalsModel : IFishVitalsModel
    {
        public float MaxHealth { get; }

        public float Health { get; private set; }

        public float MaxSaturation { get; }

        public float Saturation { get; private set; }

        public float HealthNormalized => Health / MaxHealth;

        public float SaturationNormalized => Saturation / MaxSaturation;

        public bool IsDead { get; private set; }

        public event Action Changed;

        public event Action Died;

        public FishVitalsModel(IFishVitalsConfig config)
        {
            MaxHealth = Mathf.Max(1f, config.MaxHealth);
            Health = MaxHealth;

            MaxSaturation = Mathf.Max(1f, config.MaxSaturation);
            Saturation = MaxSaturation;
        }

        public void ChangeHealth(float delta)
        {
            if (IsDead)
                return;

            var value = Mathf.Clamp(Health + delta, 0f, MaxHealth);
            if (value == Health)
                return;

            Health = value;
            Changed?.Invoke();

            if (Health > 0f)
                return;

            IsDead = true;
            Died?.Invoke();
        }

        public void ChangeSaturation(float delta)
        {
            if (IsDead)
                return;

            var value = Mathf.Clamp(Saturation + delta, 0f, MaxSaturation);
            if (value == Saturation)
                return;

            Saturation = value;
            Changed?.Invoke();
        }
    }
}
