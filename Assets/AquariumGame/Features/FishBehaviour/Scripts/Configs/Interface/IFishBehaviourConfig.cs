namespace AquariumGame.Features.FishBehaviour.Scripts.Configs.Interface
{
    public interface IFishBehaviourConfig
    {
        float SeekSpeedMultiplier { get; }

        float DeadFloatSpeed { get; }

        float DeadRemoveDelay { get; }
    }
}
