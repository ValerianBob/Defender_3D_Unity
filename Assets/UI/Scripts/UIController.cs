using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private HeroController _heroController;

    [SerializeField] private TMP_Text HeroNameText;

    [SerializeField] private TMP_Text AttackDamgeText;
    [SerializeField] private TMP_Text MagicDamgeText;

    [SerializeField] private TMP_Text AttackRangeText;

    [SerializeField] private TMP_Text MoveSpeedText;
    [SerializeField] private TMP_Text AttackSpeedText;

    [SerializeField] private TMP_Text HealthText;
    [SerializeField] private TMP_Text ManaText;

    [SerializeField] private TMP_Text HealthGainText;
    [SerializeField] private TMP_Text ManaGainText;

    [SerializeField] private TMP_Text LvText;
    [SerializeField] private TMP_Text XPText;

    [SerializeField] private Slider HealthSlider;
    [SerializeField] private Slider ManaSlider;

    [SerializeField] private Slider XpSlider;

    private void Start()
    {
        HeroNameText.text = _heroController.Hero_Attributes.HeroName;

        AttackDamgeText.text = _heroController.Hero_Attributes.CurrentDamage.ToString();
        MagicDamgeText.text = _heroController.Hero_Attributes.CurrentMagicDamage.ToString();

        AttackRangeText.text = _heroController.Hero_Attributes.CurrentAttackRange.ToString();

        MoveSpeedText.text = _heroController.Hero_Attributes.CurrentMoveSpeed.ToString();
        AttackSpeedText.text = _heroController.Hero_Attributes.CurrentAttackSpeed.ToString();

        HealthText.text = $"{_heroController.Hero_Attributes.CurrentHealth} / {_heroController.Hero_Attributes.MaxHealth}";
        ManaText.text = $"{_heroController.Hero_Attributes.CurrentMana} / {_heroController.Hero_Attributes.MaxMana}";

        HealthGainText.text = $"+{_heroController.Hero_Attributes.HealthGain}";
        ManaGainText.text = $"+{_heroController.Hero_Attributes.ManaGain}";

        LvText.text = $"Lv. {_heroController.Hero_Attributes.Lv}";
        XPText.text = $"{_heroController.Hero_Attributes.CurrentXP} / {_heroController.Hero_Attributes.XPForLevelUP}";

        HealthSlider.value = _heroController.Hero_Attributes.CurrentHealth;
        ManaSlider.value = _heroController.Hero_Attributes.CurrentMana;

        XpSlider.value = _heroController.Hero_Attributes.CurrentXP;
    }

    private void OnEnable()
    {
        HeroEvents.OnHealthChange += ChangeHealthInfo;
        HeroEvents.OnManaChange += ChangeManaInfo;
        HeroEvents.OnXpGain += ChangeXpInfo;
    }

    private void OnDisable()
    {
        HeroEvents.OnHealthChange -= ChangeHealthInfo;
        HeroEvents.OnManaChange -= ChangeManaInfo;
        HeroEvents.OnXpGain -= ChangeXpInfo;
    }


    public void ChangeHealthInfo(float currentHealth, float maxHealth)
    {
        HealthSlider.value = currentHealth / maxHealth;

        HealthText.text = $"{currentHealth} / {maxHealth}";
    }

    public void ChangeManaInfo(float currentMana, float MaxMana)
    {
        ManaSlider.value = currentMana / MaxMana;

        ManaText.text = $"{currentMana} / {MaxMana}";
    }

    public void ChangeXpInfo(float currentXp, float MaxXpForLevelUp)
    {
        XpSlider.value = currentXp / MaxXpForLevelUp;

        XPText.text = $"{currentXp} / {MaxXpForLevelUp}";
    }
}
