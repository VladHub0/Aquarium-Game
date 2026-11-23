using AquariumGame.Features.Wallet.Scripts.Model;
using AquariumGame.Features.Wallet.Scripts.Model.Interface;
using Zenject;

namespace AquariumGame.Features.Wallet.Scripts.Installers
{
    public sealed class WalletInstaller : Installer<WalletInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<IWalletModel>().To<WalletModel>().AsSingle();
        }
    }
}
