using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
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

        SetSkillsLevelUpIconsDefault();
    }

    private void OnEnable()
    {
        HeroEvents.OnHealthChangeHenlder += ChangeHealthInfo;
        HeroEvents.OnManaChangeHendler += ChangeManaInfo;

        HeroEvents.OnXpGainHendler += ChangeXpInfo;
        HeroEvents.OnLevelUpHendler += ChangeLevelUpInfo;
        HeroEvents.OnSkillLevelUpHendler += LevelUpSkill;
    }

    private void OnDisable()
    {
        HeroEvents.OnHealthChangeHenlder -= ChangeHealthInfo;
        HeroEvents.OnManaChangeHendler -= ChangeManaInfo;

        HeroEvents.OnXpGainHendler -= ChangeXpInfo;
        HeroEvents.OnLevelUpHendler -= ChangeLevelUpInfo;
        HeroEvents.OnSkillLevelUpHendler -= LevelUpSkill;
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

    public void ChangeLevelUpInfo(int[] LevelsOfSkills, HeroAttributes CurrentHeroAttributes)
    {
        UpdateAttributesInfo(CurrentHeroAttributes);

        if (CurrentHeroAttributes.Lv > 1)
        {
            ChangeSkillsLevelUp(LevelsOfSkills);
        }
    }

    public void ChangeSkillsLevelUp(int[] LevelsOfSkills)
    {
        for (int i = 0; i < Skills_LevelUp_Icons.Length; i++)
        {
            int level = LevelsOfSkills[i];

            if (level < Skills_LevelUp_Icons[i].Length)
            {
                Skills_LevelUp_Icons[i][level].texture = PlusIcon;
                Skills_LevelUp_Icons[i][level].color = UppedSkillIcon;
            }
        }
    }

    public void LevelUpSkill(int SkillId, int SkillLevelId, int PointsForLevelUpSkill, int[] HeroLevelsOfSkills)
    {
        if (PointsForLevelUpSkill > 0)
        {
            ChangeSkillsLevelUp(HeroLevelsOfSkills);
        }
        else
        {
            ClearAllSkillsLevelUps(HeroLevelsOfSkills);
        }

        Skills_LevelUp_Icons[SkillId][SkillLevelId].texture = null;
        Skills_LevelUp_Icons[SkillId][SkillLevelId].color = UppedSkillIcon;
    }

    public void UpdateAttributesInfo(HeroAttributes currentHeroAttributes)
    {
        HeroNameText.text = currentHeroAttributes.HeroName;

        AttackDamgeText.text = currentHeroAttributes.CurrentDamage.ToString();
        MagicDamgeText.text = currentHeroAttributes.CurrentMagicDamage.ToString();

        AttackRangeText.text = currentHeroAttributes.CurrentAttackRange.ToString();

        MoveSpeedText.text = currentHeroAttributes.CurrentMoveSpeed.ToString();
        AttackSpeedText.text = currentHeroAttributes.CurrentAttackSpeed.ToString();

        HealthGainText.text = $"+{currentHeroAttributes.HealthGain}";
        ManaGainText.text = $"+{currentHeroAttributes.ManaGain}";

        LvText.text = $"Lv. {currentHeroAttributes.Lv}";
    }

    public void ClearAllSkillsLevelUps(int[] HeroLevelsOfSkills)
    {
        for (int i = 0; i < Skills_LevelUp_Icons.Length; i++)
        {
            for (int j = HeroLevelsOfSkills[i]; j < Skills_LevelUp_Icons[i].Length; j++)
            {
                Skills_LevelUp_Icons[i][j].texture = null;
                Skills_LevelUp_Icons[i][j].color = EmptySkillIcon;
            }
        }

        Debug.Log("Skills level up ui was cleaned");
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
