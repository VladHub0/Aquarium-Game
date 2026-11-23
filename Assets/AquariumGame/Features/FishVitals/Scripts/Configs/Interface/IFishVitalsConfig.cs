namespace AquariumGame.Features.FishVitals.Scripts.Configs.Interface
{
    public interface IFishVitalsConfig
    {
        float MaxHealth { get; }

        float MaxSaturation { get; }

        float SaturationDecayPerSecond { get; }

        float StarvationDamagePerSecond { get; }

        float RegenerationPerSecond { get; }

        float HungryThreshold { get; }

        float FullThreshold { get; }
    }
}
