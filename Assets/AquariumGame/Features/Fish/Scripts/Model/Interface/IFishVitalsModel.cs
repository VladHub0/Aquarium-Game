
namespace AquariumGame.Features.Fish.Scripts.Model.Interface
{
    public interface IFishVitalsModel
    {
        float MaxHealth { get; set; }
        float Health { get; set; }

        float MaxSaturation { get; set; }
        float Saturation { get; set; }
        float SaturationTick { get; set; }
    }
}
