using System;
using UnityEngine;

namespace AquariumGame.Features.Shop.Scripts.View
{
    [Serializable]
    public struct ShopItemIcon
    {
        [SerializeField] private string key;
        [SerializeField] private Sprite icon;

        public string Key => key;

        public Sprite Icon => icon;
    }
}
