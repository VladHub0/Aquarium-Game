using System;
using Zenject;

namespace AquariumGame.Infrastructure.StateMachine
{
    public interface IStateFactory
    {
        IState Create(Type stateType);
    }

    /// <summary>
    /// Создаёт новый экземпляр состояния на каждый вход и внедряет зависимости через Zenject.
    /// Состояния не нужно регистрировать в контейнере вручную.
    /// </summary>
    public sealed class StateFactory : IStateFactory
    {
        private readonly DiContainer _container;

        public StateFactory(DiContainer container) => _container = container;

        public IState Create(Type stateType) => (IState)_container.Instantiate(stateType);
    }
}
