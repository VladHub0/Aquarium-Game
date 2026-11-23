using AquariumGame.Features.AssetManagement.Scripts.Services;
using AquariumGame.Features.Bootstrap.Scripts.EntryPoint;
using AquariumGame.Features.Fish.Scripts.Services.Factory;
using AquariumGame.Features.Fish.Scripts.Services.Registry;
using AquariumGame.Features.Fish.Scripts.Services.Spawner;
using AquariumGame.Features.Fish.Scripts.Services.Waypoints;
using AquariumGame.Features.Gameplay.Scripts.Services;
using AquariumGame.Features.Pause.Scripts.Services;
using AquariumGame.Features.SceneLoading.Scripts.Services;
using AquariumGame.Features.StateMachine.Scripts.Core;
using AquariumGame.Features.StateMachine.Scripts.Factory.Interface;
using AquariumGame.Features.StateMachine.Scripts.GameFlow.Interface;
using Zenject;

namespace AquariumGame.Features.Bootstrap.Scripts.Installers
{
    public sealed class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindServices();
            BindStateMachine();
            BindGameplay();

            Container.BindInterfacesTo<GameEntryPoint>().AsSingle();
        }

        private void BindServices()
        {
            Container.BindInterfacesTo<AddressablesAssetProvider>().AsSingle();
            Container.BindInterfacesTo<SceneLoader>().AsSingle();
            Container.BindInterfacesTo<GameFlowService>().AsSingle();
            Container.BindInterfacesTo<PauseInputService>().AsSingle();
        }

        private void BindStateMachine()
        {
            Container.Bind<IStateFactory>().To<StateFactory>().AsSingle();
            Container.Bind<IGameStateMachine>().To<GameStateMachine>().AsSingle();
        }

        private void BindGameplay()
        {
            Container.BindInterfacesTo<FishFactory>().AsSingle();
            Container.BindInterfacesTo<FishRegistry>().AsSingle();
            Container.BindInterfacesTo<FishSpawner>().AsSingle();
            Container.BindInterfacesTo<FishWaypointsService>().AsSingle();
            Container.BindInterfacesTo<GameplaySession>().AsSingle();
        }
    }
}
