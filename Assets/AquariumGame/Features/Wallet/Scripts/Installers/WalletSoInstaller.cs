using AquariumGame.Features.Wallet.Scripts.Configs;
using AquariumGame.Features.Wallet.Scripts.Configs.Interface;
using UnityEngine;
using Zenject;

namespace AquariumGame.Features.Wallet.Scripts.Installers
{
    [CreateAssetMenu(fileName = "WalletSoInstaller", menuName = "Installers/WalletSoInstaller")]
    public sealed class WalletSoInstaller : ScriptableObjectInstaller<WalletSoInstaller>
    {
        [SerializeField] private WalletConfig _walletConfig;

        public override void InstallBindings()
        {
            Container.Bind<IWalletConfig>().FromInstance(_walletConfig).AsSingle();
            WalletInstaller.Install(Container);
        }
    }
}
