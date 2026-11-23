using AquariumGame.Features.FishTap.Scripts.Configs;
using AquariumGame.Features.FishTap.Scripts.Configs.Interface;
using UnityEngine;
using Zenject;

namespace AquariumGame.Features.FishTap.Scripts.Installers
{
    [CreateAssetMenu(fileName = "FishTapSoInstaller", menuName = "Installers/FishTapSoInstaller")]
    public sealed class FishTapSoInstaller : ScriptableObjectInstaller<FishTapSoInstaller>
    {
        [SerializeField] private FishTapConfig _fishTapConfig;

        public override void InstallBindings()
        {
            Container.Bind<IFishTapConfig>().FromInstance(_fishTapConfig).AsSingle();
            FishTapInstaller.Install(Container);
        }
    }
}
