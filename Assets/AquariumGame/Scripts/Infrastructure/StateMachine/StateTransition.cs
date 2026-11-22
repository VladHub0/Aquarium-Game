using System;

namespace AquariumGame.Infrastructure.StateMachine
{
    /// <summary>
    /// Описание перехода: в какое состояние идти и какие данные передать.
    /// Создаётся через статический класс <see cref="Transition"/>.
    /// </summary>
    public readonly struct StateTransition
    {
        public readonly Type StateType;
        internal readonly Action<IState> ApplyPayload;

        public bool IsExit => StateType == null;

        internal StateTransition(Type stateType, Action<IState> applyPayload)
        {
            StateType = stateType;
            ApplyPayload = applyPayload;
        }
    }

    public static class Transition
    {
        /// <summary>Перейти в состояние TState.</summary>
        public static StateTransition GoTo<TState>() where TState : class, IState =>
            new StateTransition(typeof(TState), null);

        /// <summary>Перейти в состояние TState и передать ему payload.</summary>
        public static StateTransition GoTo<TState, TPayload>(TPayload payload)
            where TState : class, IPayloadState<TPayload> =>
            new StateTransition(typeof(TState), state => ((TState)state).SetPayload(payload));

        /// <summary>Завершить работу state machine.</summary>
        public static StateTransition Exit() => default;
    }
}
