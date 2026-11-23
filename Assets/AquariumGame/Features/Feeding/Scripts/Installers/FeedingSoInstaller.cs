using AquariumGame.Features.Feeding.Scripts.Configs;
using AquariumGame.Features.Feeding.Scripts.Configs.Interface;
using UnityEngine;
using Zenject;

namespace AquariumGame.Features.Feeding.Scripts.Installers
{
    [CreateAssetMenu(fileName = "FeedingSoInstaller", menuName = "Installers/FeedingSoInstaller")]
    public sealed class FeedingSoInstaller : ScriptableObjectInstaller<FeedingSoInstaller>
    {
        [SerializeField] private FeedingConfig _feedingConfig;

        public override void InstallBindings()
        {
            Container.Bind<IFeedingConfig>().FromInstance(_feedingConfig).AsSingle();
            FeedingInstaller.Install(Container);
        }
    }
}
