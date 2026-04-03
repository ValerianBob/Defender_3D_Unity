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

    public void ChangeHealthSliderAndText(float CurrentHealth, float maxHealth)
    {
        HealthSlider.value = CurrentHealth / maxHealth;

        HealthText.text = $"{CurrentHealth} / {maxHealth}";
    }

    public void ChangeManaSliderAndText(float CurrentMana, float MaxMana)
    {
        ManaSlider.value = CurrentMana / MaxMana;

        ManaText.text = $"{CurrentMana} / {MaxMana}";
    }

    public void ChangeHealthGainText(float HealthGain)
    {
        HealthGainText.text = $"+{HealthGain}";
    }

    public void ChangeManaGainText(float ManaGain)
    {
        ManaGainText.text = $"+{ManaGain}";
    }

    private void OnHeroSelectWrapper(SkillConfig[] SkillsData, int[] LevelsOfSkills, HeroAttributes currentHeroAttributes,
        HeroInventory heroInventory)
    {
        ChangeHealthSliderAndText(currentHeroAttributes.CurrentHealth, currentHeroAttributes.MaxHealth);
        ChangeManaSliderAndText(currentHeroAttributes.CurrentMana, currentHeroAttributes.MaxMana);

        ChangeHealthGainText(currentHeroAttributes.HealthGain);
        ChangeManaGainText(currentHeroAttributes.ManaGain);
    }

    private void OnEnable()
    {
        HeroEvents.OnHeroSelectHendler += OnHeroSelectWrapper;

        HeroEvents.OnHealthChangeHenlder += ChangeHealthSliderAndText;
        
        HeroEvents.OnManaChangeHendler += ChangeManaSliderAndText;
    }

    private void OnDisable()
    {
        HeroEvents.OnHeroSelectHendler += OnHeroSelectWrapper;

        HeroEvents.OnHealthChangeHenlder -= ChangeHealthSliderAndText;

        HeroEvents.OnManaChangeHendler -= ChangeManaSliderAndText;
    }
}
