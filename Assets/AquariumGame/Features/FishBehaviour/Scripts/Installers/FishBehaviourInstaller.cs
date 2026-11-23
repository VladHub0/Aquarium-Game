using AquariumGame.Features.FishBehaviour.Scripts.Core;
using AquariumGame.Features.FishBehaviour.Scripts.Core.Interface;
using AquariumGame.Features.FishBehaviour.Scripts.Factory;
using AquariumGame.Features.FishBehaviour.Scripts.Factory.Interface;
using Zenject;

namespace AquariumGame.Features.FishBehaviour.Scripts.Installers
{
    public sealed class FishBehaviourInstaller : Installer<FishBehaviourInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<IFishStateFactory>().To<FishStateFactory>().AsSingle();
            Container.Bind<IFishStateMachine>().To<FishStateMachine>().AsSingle();
        }
    }
}
