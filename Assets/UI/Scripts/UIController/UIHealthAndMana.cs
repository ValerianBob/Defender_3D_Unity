using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthAndMana : MonoBehaviour
{
    [SerializeField] private TMP_Text HealthText;
    [SerializeField] private TMP_Text HealthGainText;

    [SerializeField] private TMP_Text ManaText;
    [SerializeField] private TMP_Text ManaGainText;

    [SerializeField] private Slider HealthSlider;
    [SerializeField] private Slider ManaSlider;

    private void ChangeHealthSliderAndText(HeroEventsArgs HeroArgs)
    {
        HealthSlider.value = HeroArgs.CurrentHeroAttributes.CurrentHealth / HeroArgs.CurrentHeroAttributes.MaxHealth;

        HealthText.text = $"{HeroArgs.CurrentHeroAttributes.CurrentHealth} / {HeroArgs.CurrentHeroAttributes.MaxHealth}";
    }

    private void ChangeManaSliderAndText(HeroEventsArgs HeroArgs)
    {
        ManaSlider.value = HeroArgs.CurrentHeroAttributes.CurrentMana / HeroArgs.CurrentHeroAttributes.MaxMana;

        ManaText.text = $"{HeroArgs.CurrentHeroAttributes.CurrentMana} / {HeroArgs.CurrentHeroAttributes.MaxMana}";
    }

    private void ChangeHealthGainText(HeroEventsArgs HeroArgs)
    {
        HealthGainText.text = $"+{HeroArgs.CurrentHeroAttributes.HealthGain}";
    }

    private void ChangeManaGainText(HeroEventsArgs HeroArgs)
    {
        ManaGainText.text = $"+{HeroArgs.CurrentHeroAttributes.ManaGain}";
    }

    private void UpdateHealthAndManaInfo(HeroEventsArgs HeroArgs)
    {
        ChangeHealthSliderAndText(HeroArgs);
        ChangeManaSliderAndText(HeroArgs);
        ChangeHealthGainText(HeroArgs);
        ChangeManaGainText(HeroArgs);
    }

    private void OnEnable()
    {
        HeroEvents.OnHeroSelectHandler += UpdateHealthAndManaInfo;

        HeroEvents.OnHealthChangeHandler += ChangeHealthSliderAndText;
        HeroEvents.OnManaChangeHandler += ChangeManaSliderAndText;

        HeroEvents.OnLevelUpHandler += UpdateHealthAndManaInfo;
    }

    private void OnDisable()
    {
        HeroEvents.OnHeroSelectHandler -= UpdateHealthAndManaInfo;

        HeroEvents.OnHealthChangeHandler -= ChangeHealthSliderAndText;
        HeroEvents.OnManaChangeHandler -= ChangeManaSliderAndText;

        HeroEvents.OnLevelUpHandler -= UpdateHealthAndManaInfo;
    }
}
