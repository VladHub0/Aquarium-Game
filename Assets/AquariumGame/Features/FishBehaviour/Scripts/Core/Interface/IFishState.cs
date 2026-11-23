namespace AquariumGame.Features.FishBehaviour.Scripts.Core.Interface
{
    public interface IFishState
    {
        void Enter();

        void Tick(float deltaTime);

        void Exit();
    }
}
