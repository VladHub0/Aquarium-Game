using AquariumGame.Features.Feeding.Scripts.Model;
using AquariumGame.Features.Feeding.Scripts.Services.Factory;
using AquariumGame.Features.Feeding.Scripts.Services.Food;
using AquariumGame.Features.Feeding.Scripts.Services.Tap;
using Zenject;

namespace AquariumGame.Features.Feeding.Scripts.Installers
{
    public sealed class FeedingInstaller : Installer<FeedingInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<FeedingModeModel>().AsSingle();
            Container.BindInterfacesTo<FoodFactory>().AsSingle();
            Container.BindInterfacesTo<FoodService>().AsSingle();
            Container.BindInterfacesTo<FeedingTapHandler>().AsSingle();
        }
    }
}
