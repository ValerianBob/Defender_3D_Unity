using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIShop : MonoBehaviour
{
    [SerializeField] private GameObject ShopWindow;

    [SerializeField] private Button ShopButtonToggle;

    [Serializable]
    private struct ItemShopSlot
    {
        public RawImage ItemIcon;
        public TMP_Text ItemCostText;
        public ItemConfig Item;
        public Button BuyButton;

        public InfoWindowTriggerUI InfoWindowTrigger;
    }

    [SerializeField] private ItemShopSlot[] ItemShopSlots;

    public bool isOpened = false;

    private void Start()
    {
        ShopButtonToggle.onClick.AddListener(ToggleShopWindow);

        foreach (ItemShopSlot slot in ItemShopSlots)
        {
            slot.ItemIcon.texture = slot.Item.GetItemIcon().texture;
            slot.ItemCostText.text = slot.Item.GetItemCost().ToString();
            slot.BuyButton.onClick.AddListener(() => BuyItem(slot.Item));

            slot.InfoWindowTrigger.Initialize(slot.Item);
        }

        ShopWindow.SetActive(false);
    }

    private void ToggleShopWindow()
    {
        isOpened = !isOpened;

        ShopWindow.SetActive(isOpened);
    }

    private void BuyItem(ItemConfig Item)
    {
        UIEvents.OnItemBuyUIHandler?.Invoke(Item);
    }
}
