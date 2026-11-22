
using AquariumGame.Features.Fish.Scripts.Model;
using AquariumGame.Features.Fish.Scripts.Model.Interface;
using AquariumGame.Features.Fish.Scripts.Presenter;
using AquariumGame.Features.Fish.Scripts.Services.Facade;
using AquariumGame.Features.Fish.Scripts.View;
using AquariumGame.Features.Fish.Scripts.View.Interface;
using Zenject;

namespace AquariumGame.Features.Fish.Scripts.Installers
{
    public class FishInstaller : Installer<FishInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<FishFacade>().AsSingle();

            InstallModel();
            InstallView();
            InstallPresenters();
        }

        private void InstallModel()
        {
            Container.Bind<IFishMovementModel>().To<FishMovementModel>().AsSingle();
            Container.Bind<IFishVitalsModel>().To<FishVitalsModel>().AsSingle();
        }

        private void InstallView()
        {
            Container.Bind<IFishView>().To<FishView>().FromComponentInHierarchy().AsSingle();
        }

        private void InstallPresenters()
        {
            Container.BindInterfacesTo<FishMovementPresenter>().AsSingle();
        }
    }
}