using Zenject;
using UnityEngine;
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