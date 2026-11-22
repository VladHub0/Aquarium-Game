using System.Threading;
using Cysharp.Threading.Tasks;

namespace AquariumGame.Infrastructure.StateMachine
{
    public interface IState
    {
        UniTask Enter(CancellationToken token);
        UniTask<StateTransition> Execute(CancellationToken token);
        UniTask Exit(CancellationToken token);
    }

    public interface IPayloadState<in TPayload> : IState
    {
        void SetPayload(TPayload payload);
    }
}
