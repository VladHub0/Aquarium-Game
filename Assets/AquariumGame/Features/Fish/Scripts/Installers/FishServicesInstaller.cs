using AquariumGame.Features.Fish.Scripts.Services.Factory;
using AquariumGame.Features.Fish.Scripts.Services.Registry;
using AquariumGame.Features.Fish.Scripts.Services.Spawner;
using AquariumGame.Features.Fish.Scripts.Services.Waypoints;
using Zenject;

namespace AquariumGame.Features.Fish.Scripts.Installers
{
    public sealed class FishServicesInstaller : Installer<FishServicesInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<FishFactory>().AsSingle();
            Container.BindInterfacesTo<FishRegistry>().AsSingle();
            Container.BindInterfacesTo<FishSpawner>().AsSingle();
            Container.BindInterfacesTo<FishWaypointsService>().AsSingle();
        }
    }
}
