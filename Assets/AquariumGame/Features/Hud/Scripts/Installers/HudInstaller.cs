using AquariumGame.Features.Hud.Scripts.Presenter;
using AquariumGame.Features.Hud.Scripts.View;
using AquariumGame.Features.Hud.Scripts.View.Interface;
using Zenject;

namespace AquariumGame.Features.Hud.Scripts.Installers
{
    public sealed class HudInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IHudBarView>().To<HudBarView>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesTo<HudBarPresenter>().AsSingle();
        }
    }
}
