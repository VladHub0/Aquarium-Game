using System.Collections.Generic;
using AquariumGame.Features.Fish.Scripts.Services.Facade.Interface;
using AquariumGame.Features.Fish.Scripts.Services.Registry.Interface;
using UnityEngine;
using Zenject;

namespace AquariumGame.Features.Fish.Scripts.Services.Registry
{
    public sealed class FishRegistry : IFishRegistry, ITickable
    {
        private readonly List<IFishFacade> _fish = new List<IFishFacade>();

        public int Count => _fish.Count;

        public IReadOnlyList<IFishFacade> All => _fish;

        public void Add(IFishFacade fish)
        {
            if (fish != null && !_fish.Contains(fish))
                _fish.Add(fish);
        }

        public void Remove(IFishFacade fish) => _fish.Remove(fish);

        public void Tick()
        {
            var deltaTime = Time.deltaTime;

            for (var i = _fish.Count - 1; i >= 0; i--)
                if (i < _fish.Count)
                    _fish[i].Tick(deltaTime);
        }
    }
}
