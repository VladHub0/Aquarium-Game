using AquariumGame.Features.AssetManagement.Scripts.Services;
using Zenject;

namespace AquariumGame.Features.AssetManagement.Scripts.Installers
{
    public sealed class AssetManagementInstaller : Installer<AssetManagementInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<AddressablesAssetProvider>().AsSingle();
        }
    }
}
