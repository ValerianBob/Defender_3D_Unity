using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    //Maybe delete later HeroController :
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

    [SerializeField] private RawImage[] Skill_1_LevelUp_Icons;
    [SerializeField] private RawImage[] Skill_2_LevelUp_Icons;
    [SerializeField] private RawImage[] Skill_3_LevelUp_Icons;
    [SerializeField] private RawImage[] Skill_4_LevelUp_Icons;

    private RawImage[][] Skills_LevelUp_Icons;

    private Color UppedSkillIcon = new Color(255, 255, 0, 255);
    private Color EmptySkillIcon = new Color(0, 0, 0, 0);

    [SerializeField] private Texture PlusIcon;

    private void Start()
    {
        Skills_LevelUp_Icons = new RawImage[][]
        {
            Skill_1_LevelUp_Icons,
            Skill_2_LevelUp_Icons,
            Skill_3_LevelUp_Icons,
            Skill_4_LevelUp_Icons
        };

        HeroNameText.text = _heroController.Hero_Attributes.HeroName;

        UpdateAttributesInfo();

        ChangeHealthInfo(_heroController.Hero_Attributes.CurrentHealth, _heroController.Hero_Attributes.MaxHealth);
        ChangeManaInfo(_heroController.Hero_Attributes.CurrentMana, _heroController.Hero_Attributes.MaxMana);

        ChangeXpInfo(_heroController.Hero_Attributes.CurrentXP, _heroController.Hero_Attributes.XPForLevelUP);

        SetSkillsLevelUpIconsDefault();
    }

    private void OnEnable()
    {
        HeroEvents.OnHealthChange += ChangeHealthInfo;
        HeroEvents.OnManaChange += ChangeManaInfo;
        HeroEvents.OnXpGain += ChangeXpInfo;
        HeroEvents.OnLevelUp += ChangeLevelUp;
    }

    private void OnDisable()
    {
        HeroEvents.OnHealthChange -= ChangeHealthInfo;
        HeroEvents.OnManaChange -= ChangeManaInfo;
        HeroEvents.OnXpGain -= ChangeXpInfo;
        HeroEvents.OnLevelUp -= ChangeLevelUp;
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

    public void ChangeXpInfo(float currentXp, float XpForLevelUp)
    {
        XpSlider.value = currentXp / XpForLevelUp;

        XPText.text = $"{currentXp} / {XpForLevelUp}";
    }

    public void ChangeLevelUp(int Lv, int LevelOfSkill_1, int LevelOfSkill_2, int LevelOfSkill_3, int LevelOfSkill_4)
    {
        UpdateAttributesInfo();

        ChangeHealthInfo(_heroController.Hero_Attributes.CurrentHealth, _heroController.Hero_Attributes.MaxHealth);
        ChangeManaInfo(_heroController.Hero_Attributes.CurrentMana, _heroController.Hero_Attributes.MaxMana);

        ChangeSkillsLevelUp(LevelOfSkill_1, LevelOfSkill_2, LevelOfSkill_3, LevelOfSkill_4);
    }

    //todo
    public void ChangeSkillsLevelUp(int LevelOfSkill_1, int LevelOfSkill_2, int LevelOfSkill_3, int LevelOfSkill_4)
    {
        int[] SkillLevels = 
        {
            LevelOfSkill_1,
            LevelOfSkill_2,
            LevelOfSkill_3,
            LevelOfSkill_4
        };

        for (int i = 0; i < Skills_LevelUp_Icons.Length; i++)
        {
            int level = SkillLevels[i];

            Skills_LevelUp_Icons[i][level].texture = PlusIcon;
            Skills_LevelUp_Icons[i][level].color = UppedSkillIcon;
        }
    }

    public void LevelUpSkill(int SkillId, int SkillLevelId)
    {
        if (SkillId == 1)
        {
            Skill_1_LevelUp_Icons[SkillLevelId].texture = null;
        }
    }

    public void UpdateAttributesInfo()
    {
        AttackDamgeText.text = _heroController.Hero_Attributes.CurrentDamage.ToString();
        MagicDamgeText.text = _heroController.Hero_Attributes.CurrentMagicDamage.ToString();

        AttackRangeText.text = _heroController.Hero_Attributes.CurrentAttackRange.ToString();

        MoveSpeedText.text = _heroController.Hero_Attributes.CurrentMoveSpeed.ToString();
        AttackSpeedText.text = _heroController.Hero_Attributes.CurrentAttackSpeed.ToString();

        HealthGainText.text = $"+{_heroController.Hero_Attributes.HealthGain}";
        ManaGainText.text = $"+{_heroController.Hero_Attributes.ManaGain}";

        LvText.text = $"Lv. {_heroController.Hero_Attributes.Lv}";
    }

    public void SetSkillsLevelUpIconsDefault()
    {
        foreach(var icon in Skill_1_LevelUp_Icons)
        {
            icon.color = EmptySkillIcon;
        }
        foreach (var icon in Skill_2_LevelUp_Icons)
        {
            icon.color = EmptySkillIcon;
        }
        foreach (var icon in Skill_3_LevelUp_Icons)
        {
            icon.color = EmptySkillIcon;
        }
        foreach (var icon in Skill_4_LevelUp_Icons)
        {
            icon.color = EmptySkillIcon;
        }
    }   
}
