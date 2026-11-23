using System.Threading;
using AquariumGame.Features.AssetManagement.Scripts.Keys;
using AquariumGame.Features.AssetManagement.Scripts.Services.Interface;
using AquariumGame.Features.MainMenu.Scripts.States;
using AquariumGame.Features.StateMachine.Scripts.Core;
using Cysharp.Threading.Tasks;

namespace AquariumGame.Features.AssetManagement.Scripts.States
{
    public sealed class PreloadAssetsState : StateBase
    {
        private readonly IAssetProvider _assets;

        public PreloadAssetsState(IAssetProvider assets) => _assets = assets;

        public override async UniTask<StateTransition> Execute(CancellationToken token)
        {
            await _assets.PreloadByLabelAsync(AssetKeys.PreloadLabel, token);
            return Transition.GoTo<MainMenuState>();
        }
    }
}
