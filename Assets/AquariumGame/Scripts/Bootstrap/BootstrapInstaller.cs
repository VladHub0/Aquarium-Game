using AquariumGame.Features.Fish.Scripts.Services.Spawner;
using AquariumGame.Features.Fish.Scripts.Services.Waypoints;
using AquariumGame.Infrastructure.Services;
using AquariumGame.Infrastructure.StateMachine;
using Zenject;

namespace AquariumGame.Bootstrap
{
    public sealed class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindServices();
            BindStateMachine();
            BindGameplay();

            Container.BindInterfacesTo<GameEntryPoint>().AsSingle().NonLazy();
        }

        private void BindServices()
        {
            Container.BindInterfacesTo<AddressablesAssetProvider>().AsSingle();
            Container.BindInterfacesTo<SceneLoader>().AsSingle();
            Container.BindInterfacesTo<GameFlowService>().AsSingle();
            Container.BindInterfacesTo<PauseInputService>().AsSingle();
            Container.BindInterfacesTo<WalletService>().AsSingle();
        }

        private void BindStateMachine()
        {
            Container.Bind<IStateFactory>().To<StateFactory>().AsSingle();
            Container.Bind<IGameStateMachine>().To<GameStateMachine>().AsSingle();
        }

        private void BindGameplay()
        {
            Container.BindInterfacesTo<FishSpawner>().AsSingle();
            Container.BindInterfacesTo<FishWaypointsService>().AsSingle();
            Container.BindInterfacesTo<GameplaySession>().AsSingle();
            Container.BindInterfacesTo<FishShop>().AsSingle();
        }
    }
}
