using AquariumGame.Features.RoundTimer.Scripts.Presenter;
using AquariumGame.Features.RoundTimer.Scripts.View;
using AquariumGame.Features.RoundTimer.Scripts.View.Interface;
using Zenject;

namespace AquariumGame.Features.RoundTimer.Scripts.Installers
{
    public sealed class RoundTimerViewInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IRoundTimerView>().To<RoundTimerView>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesTo<RoundTimerPresenter>().AsSingle();
        }
    }
}
