using System;

namespace AquariumGame.Features.Feeding.Scripts.View.Interface
{
    public interface IFeedButtonView
    {
        event Action Clicked;

        void SetActive(bool active);
    }
}
