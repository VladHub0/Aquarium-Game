using AquariumGame.Features.FishVitals.Scripts.Model;
using AquariumGame.Features.FishVitals.Scripts.Model.Interface;
using AquariumGame.Features.FishVitals.Scripts.Presenter;
using AquariumGame.Features.FishVitals.Scripts.Presenter.Interface;
using AquariumGame.Features.FishVitals.Scripts.Services;
using AquariumGame.Features.FishVitals.Scripts.Services.Interface;
using AquariumGame.Features.FishVitals.Scripts.View;
using AquariumGame.Features.FishVitals.Scripts.View.Interface;
using Zenject;

namespace AquariumGame.Features.FishVitals.Scripts.Installers
{
    public sealed class FishVitalsInstaller : Installer<FishVitalsInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<IFishVitalsModel>().To<FishVitalsModel>().AsSingle();
            Container.Bind<IFishVitalsService>().To<FishVitalsService>().AsSingle();
            Container.Bind<IFishVitalsView>().To<FishVitalsView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<IFishVitalsPresenter>().To<FishVitalsPresenter>().AsSingle();
        }
    }
}
