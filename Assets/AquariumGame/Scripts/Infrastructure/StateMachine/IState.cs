using System.Threading;
using Cysharp.Threading.Tasks;

namespace AquariumGame.Infrastructure.StateMachine
{
    /// <summary>
    /// Состояние глобальной state machine.
    /// Жизненный цикл: Enter -> Execute -> Exit.
    /// Execute живёт, пока не вернёт переход в следующее состояние.
    /// </summary>
    public interface IState
    {
        UniTask Enter(CancellationToken token);
        UniTask<StateTransition> Execute(CancellationToken token);
        UniTask Exit(CancellationToken token);
    }

    /// <summary>
    /// Состояние, которое принимает данные при переходе.
    /// </summary>
    public interface IPayloadState<in TPayload> : IState
    {
        void SetPayload(TPayload payload);
    }
}
