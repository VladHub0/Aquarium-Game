using UnityEngine;

namespace AquariumGame.Features.Fish.Scripts.View.Interface
{
    public interface IFishView
    {
        Vector3 Position { get; set; }

        void LookTo(Vector3 direction, float deltaTime);

        bool ContainsPoint(Vector2 worldPoint);

        void PlayTapFeedback();

        void SetDead();
    }
}
