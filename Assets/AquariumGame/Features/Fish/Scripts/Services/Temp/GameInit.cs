using Zenject;
public class GameInit : IInitializable
{

    private readonly IFishFacade _fishFacade;

    [Inject]
    public GameInit(IFishFacade fishFacade)
    {
        _fishFacade = fishFacade;
    }

    public void Initialize()
    {
        _fishFacade.Init();
    }
}
