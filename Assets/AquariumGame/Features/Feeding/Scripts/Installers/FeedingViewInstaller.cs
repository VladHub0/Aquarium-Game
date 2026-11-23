using AquariumGame.Features.Feeding.Scripts.Presenter;
using AquariumGame.Features.Feeding.Scripts.View;
using AquariumGame.Features.Feeding.Scripts.View.Interface;
using Zenject;

namespace AquariumGame.Features.Feeding.Scripts.Installers
{
    public sealed class FeedingViewInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IFeedButtonView>().To<FeedButtonView>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesTo<FeedButtonPresenter>().AsSingle();
        }
    }
}
