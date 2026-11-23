using AquariumGame.Features.Fish.Scripts.Model;
using AquariumGame.Features.Fish.Scripts.Model.Interface;
using AquariumGame.Features.Fish.Scripts.Presenter;
using AquariumGame.Features.Fish.Scripts.Presenter.Interface;
using AquariumGame.Features.Fish.Scripts.Services.Facade;
using AquariumGame.Features.Fish.Scripts.Services.Facade.Interface;
using AquariumGame.Features.Fish.Scripts.View;
using AquariumGame.Features.Fish.Scripts.View.Interface;
using Zenject;

namespace AquariumGame.Features.Fish.Scripts.Installers
{
    public class FishInstaller : Installer<FishInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<IFishFacade>().To<FishFacade>().AsSingle();

            InstallModel();
            InstallView();
            InstallPresenters();
        }

        private void InstallModel()
        {
            Container.Bind<IFishMovementModel>().To<FishMovementModel>().AsSingle();
        }

        private void InstallView()
        {
            Container.Bind<IFishView>().To<FishView>().FromComponentInHierarchy().AsSingle();
        }

        private void InstallPresenters()
        {
            Container.Bind<IFishMovementPresenter>().To<FishMovementPresenter>().AsSingle();
        }
    }
}
