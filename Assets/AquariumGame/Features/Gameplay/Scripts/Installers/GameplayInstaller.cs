using AquariumGame.Features.Gameplay.Scripts.Services;
using Zenject;

namespace AquariumGame.Features.Gameplay.Scripts.Installers
{
    public sealed class GameplayInstaller : Installer<GameplayInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<GameplaySession>().AsSingle();
        }
    }
}
