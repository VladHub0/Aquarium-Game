using Zenject;
using UnityEngine;
using AquariumGame.Features.Fish.Scripts.Services.Facade.Interface;
using AquariumGame.Features.Fish.Scripts.Services.Temp;

namespace AquariumGame.Features.Fish.Scripts.Installers
{
    public class TempInstaller : MonoInstaller
    {
        [SerializeField] GameObject fishPrefab;
        public override void InstallBindings()
        {

            Container.BindInterfacesAndSelfTo<GameInit>().AsSingle();

            FishInstaller();
        }


        public void FishInstaller()
        {
            Container.Bind<IFishFacade>()
                .FromSubContainerResolve()
                .ByNewPrefabInstaller<FishInstaller>(fishPrefab)
                .AsSingle();
        }
    }
}