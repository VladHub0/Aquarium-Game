using Zenject;

public class FishInstaller : Installer<FishInstaller>
{
    public override void InstallBindings()
    {
        Container.BindInterfacesTo<FishFacade>().AsSingle();
    }

}