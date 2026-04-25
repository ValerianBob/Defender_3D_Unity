using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    [SerializeField] private RawImage[] ItemIcons;
    [SerializeField] private InfoWindowTriggerUI[] InfoWindowTriggers;
    [SerializeField] private TMP_Text GoldText;

    private Color SeeIconColor = new Color(255, 255, 255, 255);
    private Color EmptyIconColor = new Color(0, 0, 0, 0);

    private void SetGold(HeroEventsArgs HeroArgs)
    {
        GoldText.text = HeroArgs.CurrentHeroInventory.GetGold().ToString();
    }

    private void SetItemsIcons(HeroEventsArgs HeroArgs)
    {
        ItemConfig[] items = HeroArgs.CurrentHeroInventory.GetItems();

        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] != null)
            {
                ItemIcons[i].color = SeeIconColor;
                ItemIcons[i].texture = items[i].GetItemIcon().texture;

                InfoWindowTriggers[i].Initialize(items[i]);
            }
            else
            {
                ItemIcons[i].color = EmptyIconColor;
                ItemIcons[i].texture = null;

                InfoWindowTriggers[i].Initialize(null);
            }
        }
    }

    private void UpdateInventoryInfo(HeroEventsArgs HeroArgs)
    {
        SetGold(HeroArgs);
        SetItemsIcons(HeroArgs);
    }

    private void OnEnable()
    {
        HeroEvents.OnHeroSelectHandler += UpdateInventoryInfo;

        HeroEvents.OnItemTakeHandler += SetItemsIcons;

        HeroEvents.OnGoldGainHandler += SetGold;

        HeroEvents.OnItemDropHandler += SetItemsIcons;

        HeroEvents.OnItemSwapHandler += SetItemsIcons;
    }

    private void OnDisable()
    {
        HeroEvents.OnHeroSelectHandler -= UpdateInventoryInfo;

        HeroEvents.OnItemTakeHandler -= SetItemsIcons;

        HeroEvents.OnGoldGainHandler -= SetGold;

        HeroEvents.OnItemDropHandler -= SetItemsIcons;

        HeroEvents.OnItemSwapHandler -= SetItemsIcons;
    }
}
