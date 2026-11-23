using AquariumGame.Features.Pause.Scripts.Services;
using Zenject;

namespace AquariumGame.Features.Pause.Scripts.Installers
{
    public sealed class PauseInstaller : Installer<PauseInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<PauseInputService>().AsSingle();
        }
    }
}
