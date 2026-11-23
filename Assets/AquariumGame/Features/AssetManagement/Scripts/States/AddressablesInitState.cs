using System;
using System.Threading;
using AquariumGame.Features.AssetManagement.Scripts.Services.Interface;
using AquariumGame.Features.StateMachine.Scripts.Core;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AquariumGame.Features.AssetManagement.Scripts.States
{
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
                Debug.LogWarning($"[AddressablesInit] Не удалось обновить каталог: {e.Message}");
            }

            return Transition.GoTo<PreloadAssetsState>();
        }
    }
}
