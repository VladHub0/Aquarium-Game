using AquariumGame.Features.Shop.Scripts.Presenter;
using AquariumGame.Features.Shop.Scripts.Services;
using AquariumGame.Features.Shop.Scripts.Services.Interface;
using AquariumGame.Features.Shop.Scripts.View;
using AquariumGame.Features.Shop.Scripts.View.Interface;
using Zenject;

namespace AquariumGame.Features.Shop.Scripts.Installers
{
    public sealed class ShopInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IFishShop>().To<FishShop>().AsSingle();
            Container.Bind<IShopView>().To<ShopView>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesTo<ShopPresenter>().AsSingle();
        }
    }
}
