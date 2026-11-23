using System;

namespace AquariumGame.Features.Shop.Scripts.View.Interface
{
    public interface IShopView
    {
        event Action<int> BuyClicked;

        int AddItem(string title, int price);

        void SetItemInteractable(int index, bool interactable);
    }
}
