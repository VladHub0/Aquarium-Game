using AquariumGame.Features.AssetManagement.Scripts.Installers;
using AquariumGame.Features.Bootstrap.Scripts.EntryPoint;
using AquariumGame.Features.Fish.Scripts.Installers;
using AquariumGame.Features.GameOver.Scripts.Installers;
using AquariumGame.Features.Gameplay.Scripts.Installers;
using AquariumGame.Features.Pause.Scripts.Installers;
using AquariumGame.Features.PointerInput.Scripts.Installers;
using AquariumGame.Features.SceneLoading.Scripts.Installers;
using AquariumGame.Features.StateMachine.Scripts.Installers;
using Zenject;

namespace AquariumGame.Features.Bootstrap.Scripts.Installers
{
    public sealed class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            AssetManagementInstaller.Install(Container);
            SceneLoadingInstaller.Install(Container);
            StateMachineInstaller.Install(Container);
            PauseInstaller.Install(Container);
            PointerInputInstaller.Install(Container);
            FishServicesInstaller.Install(Container);
            GameplayInstaller.Install(Container);
            GameOverInstaller.Install(Container);

            Container.BindInterfacesTo<GameEntryPoint>().AsSingle();
        }
    }
}
