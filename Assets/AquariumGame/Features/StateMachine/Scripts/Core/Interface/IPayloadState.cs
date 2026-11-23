namespace AquariumGame.Features.StateMachine.Scripts.Core.Interface
{
    public interface IPayloadState<in TPayload> : IState
    {
        void SetPayload(TPayload payload);
    }
}
