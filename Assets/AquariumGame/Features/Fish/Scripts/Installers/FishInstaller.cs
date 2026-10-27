using AquariumGame.Features.Fish.Scripts.Services.Facade;
using Zenject;

namespace AquariumGame.Features.Fish.Scripts.Installers
{
    public class FishInstaller : Installer<FishInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<FishFacade>().AsSingle();
        }

        private void InstallFishConfigs()
        {

        }
        private void InstallFishModels()
        {

        }

        private void InstallFishServices()
        {

        }

        private void InstallFishPresenters()
        {

        }

        private void InstallFishViews()
        {

        }
    }
}