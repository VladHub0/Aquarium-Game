using System;
using System.Threading;
using AquariumGame.Infrastructure.Services;
using AquariumGame.Infrastructure.StateMachine;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AquariumGame.Infrastructure.States
{
    /// <summary>
    /// 2. Инициализация Addressables и обновление каталога.
    /// </summary>
    public sealed class AddressablesInitState : StateBase
    {
        private readonly IAssetProvider _assets;

        public AddressablesInitState(IAssetProvider assets) => _assets = assets;

        public override async UniTask<StateTransition> Execute(CancellationToken token)
        {
            await _assets.InitializeAsync(token);

            try
            {
                await _assets.UpdateCatalogsAsync(token);
            }
            catch (Exception e) when (e is not OperationCanceledException)
            {
                // Нет сети или удалённого каталога — продолжаем с локальным
                Debug.LogWarning($"[AddressablesInit] Не удалось обновить каталог: {e.Message}");
            }

            return Transition.GoTo<PreloadAssetsState>();
        }
    }
}
