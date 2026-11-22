using System.Threading;
using AquariumGame.Features.Fish.Scripts.Services.Facade.Interface;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AquariumGame.Features.Fish.Scripts.Services.Spawner.Interface
{
    public interface IFishSpawner
    {
        UniTask<IFishFacade> SpawnAsync(string key, Vector3 position, CancellationToken token);

        void DespawnAll();
    }
}
