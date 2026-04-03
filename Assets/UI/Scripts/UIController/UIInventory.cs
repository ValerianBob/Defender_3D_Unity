using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    [SerializeField] private RawImage[] ItemIcons;

    [SerializeField] private TMP_Text GoldText;

    private Color SeeIconColor = new Color(255, 255, 255, 255);

    public void SetGold(int gold)
    {
        GoldText.text = gold.ToString();
    }

    public void SetItemsIcons(ItemConfig[] items)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] != null)
            {
                ItemIcons[i].color = SeeIconColor;
                ItemIcons[i].texture = items[i].GetItemIcon().texture;
            }
        }
    }

    private void OnHeroSelectWrapper(SkillConfig[] SkillsData, int[] LevelsOfSkills, HeroAttributes currentHeroAttributes, 
        HeroInventory heroInventory)
    {
        SetGold(heroInventory.GetGold());
    }

    private void OnEnable()
    {
        HeroEvents.OnItemTakeHendler += SetItemsIcons;

        HeroEvents.OnGoldGainHendler += SetGold;
    }

    private void OnDisable()
    {
        HeroEvents.OnItemTakeHendler -= SetItemsIcons;

        HeroEvents.OnGoldGainHendler -= SetGold;
    }
}
