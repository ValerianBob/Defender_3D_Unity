using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UILevelAndXP : MonoBehaviour
{
    [SerializeField] private TMP_Text LvText;
    [SerializeField] private TMP_Text XPText;

    [SerializeField] private Slider XpSlider;

    public void ChangeXpSliderAndText(float CurrentXp, float XpForLevelUp)
    {
        XpSlider.value = CurrentXp / XpForLevelUp;

        XPText.text = $"{CurrentXp} / {XpForLevelUp}";
    }

    private void ChangeLvText(int CurrentLv)
    {
        LvText.text = $"Lv. {CurrentLv}";
    }

    private void OnHeroSelectWrapper(SkillConfig[] SkillsData, int[] LevelsOfSkills, HeroAttributes currentHeroAttributes,
        HeroInventory heroInventory)
    {
        ChangeXpSliderAndText(currentHeroAttributes.CurrentXP, currentHeroAttributes.XPForLevelUP);

        ChangeLvText(currentHeroAttributes.Lv);
    }

    private void OnEnable()
    {
        HeroEvents.OnHeroSelectHendler += OnHeroSelectWrapper;

        HeroEvents.OnXpGainHendler += ChangeXpSliderAndText;
    }

    private void OnDisable()
    {
        HeroEvents.OnHeroSelectHendler -= OnHeroSelectWrapper;

        HeroEvents.OnXpGainHendler -= ChangeXpSliderAndText;
    }
}
