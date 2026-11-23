namespace AquariumGame.Features.FishVitals.Scripts.View.Interface
{
    public interface IFishVitalsView
    {
        void SetHealth(float normalized);

        void SetSaturation(float normalized, bool hungry);

        void SetVisible(bool visible);
    }
}
