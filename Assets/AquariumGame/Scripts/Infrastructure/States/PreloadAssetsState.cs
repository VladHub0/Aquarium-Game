using System.Threading;
using AquariumGame.Infrastructure.Services;
using AquariumGame.Infrastructure.StateMachine;
using Cysharp.Threading.Tasks;

namespace AquariumGame.Infrastructure.States
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
