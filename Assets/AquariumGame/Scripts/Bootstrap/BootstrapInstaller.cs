using AquariumGame.Features.Fish.Scripts.Services.Spawner;
using AquariumGame.Infrastructure.Services;
using AquariumGame.Infrastructure.StateMachine;
using Zenject;

namespace AquariumGame.Bootstrap
{
    /// <summary>
    /// Installer для SceneContext в Bootstrap-сцене.
    /// </summary>
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
        }

        private void BindStateMachine()
        {
            // Состояния не биндятся: StateFactory создаёт их через Container.Instantiate
            Container.Bind<IStateFactory>().To<StateFactory>().AsSingle();
            Container.Bind<IGameStateMachine>().To<GameStateMachine>().AsSingle();
        }

        private void BindGameplay()
        {
            Container.BindInterfacesTo<FishSpawner>().AsSingle();
        }
    }
}
