using AquariumGame.Features.PointerInput.Scripts.Services;
using Zenject;

namespace AquariumGame.Features.PointerInput.Scripts.Installers
{
    public sealed class PointerInputInstaller : Installer<PointerInputInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<PointerInputService>().AsSingle();
        }
    }
}
