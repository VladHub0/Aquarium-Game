using AquariumGame.Features.Wallet.Scripts.Presenter;
using AquariumGame.Features.Wallet.Scripts.View;
using AquariumGame.Features.Wallet.Scripts.View.Interface;
using Zenject;

namespace AquariumGame.Features.Wallet.Scripts.Installers
{
    public sealed class WalletViewInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ICoinsView>().To<CoinsView>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesTo<CoinsPresenter>().AsSingle();
        }
    }
}
