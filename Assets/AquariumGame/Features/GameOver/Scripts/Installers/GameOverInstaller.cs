using AquariumGame.Features.GameOver.Scripts.Services;
using Zenject;

namespace AquariumGame.Features.GameOver.Scripts.Installers
{
    public sealed class GameOverInstaller : Installer<GameOverInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<GameOverWatcher>().AsSingle();
        }
    }
}
