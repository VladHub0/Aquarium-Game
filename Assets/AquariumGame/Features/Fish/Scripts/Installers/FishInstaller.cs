
using AquariumGame.Features.Fish.Scripts.Model;
using AquariumGame.Features.Fish.Scripts.Model.Interface;
using AquariumGame.Features.Fish.Scripts.Services.Facade;
using Zenject;

namespace AquariumGame.Features.Fish.Scripts.Installers
{
    public class FishInstaller : Installer<FishInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<FishFacade>().AsSingle();

            InstallModel();

        }

        private void InstallModel()
        {
            Container.Bind<IFishMovementModel>().To<FishMovementModel>().AsSingle();
            Container.Bind<IFishVitalsModel>().To<FishVitalsModel>().AsSingle();
        }
    }
}