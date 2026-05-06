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
        if (HeroArgs.CurrentHeroAttributes.MaxHealth <= 0)
        {
            return;
        }

        float current = HeroArgs.CurrentHeroAttributes.CurrentHealth;
        float max = HeroArgs.CurrentHeroAttributes.MaxHealth;

        HealthSlider.value = current / max;

        HealthText.text = $"{HeroArgs.CurrentHeroAttributes.CurrentHealth} / {HeroArgs.CurrentHeroAttributes.MaxHealth}";
    }

    private void ChangeManaSliderAndText(HeroEventsArgs HeroArgs)
    {
        if (HeroArgs.CurrentHeroAttributes.MaxMana <= 0)
        {
            return;
        }

        float current = HeroArgs.CurrentHeroAttributes.CurrentMana;
        float max = HeroArgs.CurrentHeroAttributes.MaxMana;

        ManaSlider.value = current / max;

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

        HeroEvents.OnItemTakeHandler += UpdateHealthAndManaInfo;

        HeroEvents.OnItemDropHandler += UpdateHealthAndManaInfo;
    }

    private void OnDisable()
    {
        HeroEvents.OnHeroSelectHandler -= UpdateHealthAndManaInfo;

        HeroEvents.OnHealthChangeHandler -= ChangeHealthSliderAndText;
        HeroEvents.OnManaChangeHandler -= ChangeManaSliderAndText;

        HeroEvents.OnLevelUpHandler -= UpdateHealthAndManaInfo;

        HeroEvents.OnItemTakeHandler -= UpdateHealthAndManaInfo;

        HeroEvents.OnItemDropHandler -= UpdateHealthAndManaInfo;
    }
}
