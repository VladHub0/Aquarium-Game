using System.Threading;
using AquariumGame.Features.Fish.Scripts.Services.Facade.Interface;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AquariumGame.Features.Fish.Scripts.Services.Spawner.Interface
{
    public interface IFishSpawner
    {
        /// <summary>Загружает префаб рыбы и создаёт экземпляр в активной сцене.</summary>
        UniTask<IFishFacade> SpawnAsync(Vector3 position, CancellationToken token);

        /// <summary>Уничтожает всех рыб, созданных этим спаунером.</summary>
        void DespawnAll();
    }
}
