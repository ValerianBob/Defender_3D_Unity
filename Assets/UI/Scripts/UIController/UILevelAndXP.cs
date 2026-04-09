using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UILevelAndXP : MonoBehaviour
{
    [SerializeField] private TMP_Text LvText;
    [SerializeField] private TMP_Text XPText;

    [SerializeField] private Slider XpSlider;

    private void ChangeXpSliderAndText(HeroEventsArgs HeroArgs)
    {
        XpSlider.value = HeroArgs.CurrentHeroAttributes.CurrentXP / HeroArgs.CurrentHeroAttributes.XPForLevelUP;

        XPText.text = $"{HeroArgs.CurrentHeroAttributes.CurrentXP} / {HeroArgs.CurrentHeroAttributes.XPForLevelUP}";
    }

    private void ChangeLvText(HeroEventsArgs HeroArgs)
    {
        LvText.text = $"Lv. {HeroArgs.CurrentHeroAttributes.Lv}";
    }

    private void UpdateXPandLevelInfo(HeroEventsArgs HeroArgs)
    {
        ChangeXpSliderAndText(HeroArgs);
        ChangeLvText(HeroArgs);
    }

    private void OnEnable()
    {
        HeroEvents.OnHeroSelectHandler += UpdateXPandLevelInfo;

        HeroEvents.OnXpGainHandler += ChangeXpSliderAndText;

        HeroEvents.OnLevelUpHandler += ChangeLvText;
    }

    private void OnDisable()
    {
        HeroEvents.OnHeroSelectHandler -= UpdateXPandLevelInfo;

        HeroEvents.OnXpGainHandler -= ChangeXpSliderAndText;

        HeroEvents.OnLevelUpHandler -= ChangeLvText;
    }
}
