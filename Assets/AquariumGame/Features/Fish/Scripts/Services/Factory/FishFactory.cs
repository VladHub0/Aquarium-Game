using System;
using AquariumGame.Features.Fish.Scripts.Services.Facade.Interface;
using AquariumGame.Features.Fish.Scripts.Services.Factory.Interface;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace AquariumGame.Features.Fish.Scripts.Services.Factory
{
    public sealed class FishFactory : IFishFactory
    {
        private readonly IInstantiator _instantiator;

        public FishFactory(IInstantiator instantiator) => _instantiator = instantiator;

        public FishInstance Create(GameObject prefab, Vector3 position)
        {
            var instance = _instantiator.InstantiatePrefab(prefab, position, Quaternion.identity, null);

            var context = instance.GetComponent<GameObjectContext>();
            if (context == null)
            {
                Object.Destroy(instance);
                throw new InvalidOperationException($"[FishFactory] На префабе '{prefab.name}' нет GameObjectContext.");
            }

            return new FishInstance(instance, context.Container.Resolve<IFishFacade>());
        }
    }
}
