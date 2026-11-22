

namespace AquariumGame.Features.Fish.Scripts.Configs.Interface
{
    public interface IFishConfig
    {
        float Speed { get; }

        float WaypointReachDistance { get; }

       
        float Health { get; }

        float Saturation { get; }

        float SaturationTick { get; }
    }
}
