using System;
using AquariumGame.Features.Feeding.Scripts.Model.Interface;

namespace AquariumGame.Features.Feeding.Scripts.Model
{
    public sealed class FeedingModeModel : IFeedingModeModel
    {
        public bool IsActive { get; private set; }

        public event Action<bool> Changed;

        public void SetActive(bool active)
        {
            if (IsActive == active)
                return;

            IsActive = active;
            Changed?.Invoke(active);
        }

        public void Toggle() => SetActive(!IsActive);
    }
}
