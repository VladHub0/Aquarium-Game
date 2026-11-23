using UnityEngine;

namespace AquariumGame.Features.PointerInput.Scripts.Services.Interface
{
    public interface ITapHandler
    {
        bool TryHandleTap(Vector2 worldPoint);
    }
}
