using System;

namespace AquariumGame.Features.Feeding.Scripts.Model.Interface
{
    public interface IFeedingModeModel
    {
        bool IsActive { get; }

        event Action<bool> Changed;

        void SetActive(bool active);

        void Toggle();
    }
}
