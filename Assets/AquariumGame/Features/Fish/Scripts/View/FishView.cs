using AquariumGame.Features.Fish.Scripts.View.Interface;
using UnityEngine;

namespace AquariumGame.Features.Fish.Scripts.View
{
    public sealed class FishView : MonoBehaviour, IFishView
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }

        public void LookTo(float directionX)
        {
            if (_spriteRenderer == null || Mathf.Approximately(directionX, 0f))
                return;

            _spriteRenderer.flipX = directionX < 0f;
        }
    }
}
