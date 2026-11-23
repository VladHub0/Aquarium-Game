namespace AquariumGame.Features.FishVitals.Scripts.Services.Interface
{
    public interface IFishVitalsService
    {
        bool IsHungry { get; }

        bool CanEat { get; }

        void Tick(float deltaTime);

        void Feed(float nutrition);
    }
}
