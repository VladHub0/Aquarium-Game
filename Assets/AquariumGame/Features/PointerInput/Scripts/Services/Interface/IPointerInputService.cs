namespace AquariumGame.Features.PointerInput.Scripts.Services.Interface
{
    public interface IPointerInputService
    {
        void Register(ITapHandler handler, int priority);

        void Unregister(ITapHandler handler);
    }
}
