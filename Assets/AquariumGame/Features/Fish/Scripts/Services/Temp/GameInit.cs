using AquariumGame.Features.Fish.Scripts.Services.Facade.Interface;
using Zenject;

namespace AquariumGame.Features.Fish.Scripts.Services.Temp
{
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
}
