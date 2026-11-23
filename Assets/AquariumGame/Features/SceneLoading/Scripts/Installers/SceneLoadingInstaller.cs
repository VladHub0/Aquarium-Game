using AquariumGame.Features.SceneLoading.Scripts.Services;
using Zenject;

namespace AquariumGame.Features.SceneLoading.Scripts.Installers
{
    public sealed class SceneLoadingInstaller : Installer<SceneLoadingInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<SceneLoader>().AsSingle();
        }
    }
}
