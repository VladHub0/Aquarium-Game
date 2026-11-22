using System.Threading;
using AquariumGame.Infrastructure.StateMachine;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace AquariumGame.Infrastructure.States
{
    /// <summary>
    /// 1. Базовая настройка приложения: FPS, DOTween и т.п.
    /// </summary>
    public sealed class BootstrapState : StateBase
    {
        public override async UniTask<StateTransition> Execute(CancellationToken token)
        {
            Application.targetFrameRate = 60;
            QualitySettings.vSyncCount = 0;

            DOTween.Init(recycleAllByDefault: true, useSafeMode: true, logBehaviour: LogBehaviour.ErrorsOnly);
            DOTween.SetTweensCapacity(500, 50);

            await UniTask.Yield(token);
            return Transition.GoTo<AddressablesInitState>();
        }
    }
}
