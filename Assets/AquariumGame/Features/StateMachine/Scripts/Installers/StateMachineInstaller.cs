using AquariumGame.Features.StateMachine.Scripts.Core;
using AquariumGame.Features.StateMachine.Scripts.Core.Interface;
using AquariumGame.Features.StateMachine.Scripts.Factory;
using AquariumGame.Features.StateMachine.Scripts.Factory.Interface;
using AquariumGame.Features.StateMachine.Scripts.GameFlow;
using Zenject;

namespace AquariumGame.Features.StateMachine.Scripts.Installers
{
    public sealed class StateMachineInstaller : Installer<StateMachineInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<IStateFactory>().To<StateFactory>().AsSingle();
            Container.Bind<IGameStateMachine>().To<GameStateMachine>().AsSingle();
            Container.BindInterfacesTo<GameFlowService>().AsSingle();
        }
    }
}
