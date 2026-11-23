using AquariumGame.Features.RoundTimer.Scripts.Configs;
using AquariumGame.Features.RoundTimer.Scripts.Configs.Interface;
using UnityEngine;
using Zenject;

namespace AquariumGame.Features.RoundTimer.Scripts.Installers
{
    [CreateAssetMenu(fileName = "RoundTimerSoInstaller", menuName = "Installers/RoundTimerSoInstaller")]
    public sealed class RoundTimerSoInstaller : ScriptableObjectInstaller<RoundTimerSoInstaller>
    {
        [SerializeField] private RoundTimerConfig _roundTimerConfig;

        public override void InstallBindings()
        {
            Container.Bind<IRoundTimerConfig>().FromInstance(_roundTimerConfig).AsSingle();
            RoundTimerInstaller.Install(Container);
        }
    }
}
