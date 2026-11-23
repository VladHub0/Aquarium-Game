using AquariumGame.Features.Wallet.Scripts.Configs.Interface;
using UnityEngine;

namespace AquariumGame.Features.Wallet.Scripts.Configs
{
    [CreateAssetMenu(menuName = "Aquarium/Wallet Config", fileName = "WalletConfig")]
    public sealed class WalletConfig : ScriptableObject, IWalletConfig
    {
        [SerializeField, Min(0)] private int startCoins = 300;

        public int StartCoins => startCoins;
    }
}
