using AquariumGame.Features.FishTap.Scripts.Services;
using Zenject;

namespace AquariumGame.Features.FishTap.Scripts.Installers
{
    public sealed class FishTapInstaller : Installer<FishTapInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<FishTapHandler>().AsSingle();
        }
    }
}
