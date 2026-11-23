using AquariumGame.Features.RoundTimer.Scripts.Model;
using AquariumGame.Features.RoundTimer.Scripts.Services;
using Zenject;

namespace AquariumGame.Features.RoundTimer.Scripts.Installers
{
    public sealed class RoundTimerInstaller : Installer<RoundTimerInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<RoundTimerModel>().AsSingle();
            Container.BindInterfacesTo<RoundTimerService>().AsSingle();
        }
    }
}
