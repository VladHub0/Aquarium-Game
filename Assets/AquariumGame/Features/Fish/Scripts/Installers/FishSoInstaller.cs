using AquariumGame.Features.Fish.Scripts.Configs;
using AquariumGame.Features.Fish.Scripts.Configs.Interface;
using AquariumGame.Features.Fish.Scripts.Installers;
using UnityEngine;
using Zenject;


namespace AquariumGame.Features.Fish.Scripts.Installers
{

    [CreateAssetMenu(fileName = "FishSoInstaller", menuName = "Installers/FishSoInstaller")]
    public class FishSoInstaller : ScriptableObjectInstaller<FishSoInstaller>
    {
        [SerializeField] private FishConfig _fishConfig;
        public override void InstallBindings()
        {
            InstallConfigs();
            InstallFishInstallers();
        }

        private void InstallConfigs()
        {
            Container.Bind<IFishConfig>()
              .FromInstance(_fishConfig)
              .AsSingle();
        }

        private void InstallFishInstallers()
        {
            FishInstaller.Install(Container);
        }
    }
}