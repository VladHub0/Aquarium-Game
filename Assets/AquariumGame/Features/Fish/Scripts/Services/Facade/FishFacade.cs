
using AquariumGame.Features.Fish.Scripts.Services.Facade.Interface;
using UnityEngine;


namespace AquariumGame.Features.Fish.Scripts.Services.Facade
{
    public class FishFacade : IFishFacade
    {
        public void Init()
        {
            Debug.Log("Hello From FishFacade");
        }
    }
}