using UnityEngine;
using UnityEngine.UI;

public class UISkills : MonoBehaviour
{
    [System.Serializable]
    private struct SkillPanel
    {
        [SerializeField] private RawImage skillIcon;
        [SerializeField] private GameObject keyPanel;

        public Texture SkillTexture
        {
            set
            {
                if (skillIcon != null)
                {
                    skillIcon.texture = value;
                }
            }
        }

        public void SetKeyPanelActive(bool isEnable)
        {
            keyPanel.SetActive(isEnable);
        }
    }

    [SerializeField] private SkillPanel[] SkillsPanels;

    [SerializeField] private RawImage[] Skill_1_LevelUp_Icons;
    [SerializeField] private RawImage[] Skill_2_LevelUp_Icons;
    [SerializeField] private RawImage[] Skill_3_LevelUp_Icons;
    [SerializeField] private RawImage[] Skill_4_LevelUp_Icons;

    private RawImage[][] Skills_LevelUp_Icons;

    [SerializeField] private Texture PlusIcon;

    private Color LevelImprovedIconColor = new Color(255, 255, 0, 255);
    private Color EmptyIconColor = new Color(0, 0, 0, 0);

    private void Awake()
    {
        Skills_LevelUp_Icons = new RawImage[][]
        {
            Skill_1_LevelUp_Icons,
            Skill_2_LevelUp_Icons,
            Skill_3_LevelUp_Icons,
            Skill_4_LevelUp_Icons
        };
    }

    public void ChangeSkillsLevelUp(int[] LevelsOfSkills)
    {
        for (int i = 0; i < Skills_LevelUp_Icons.Length; i++)
        {
            int level = LevelsOfSkills[i];

            if (level < Skills_LevelUp_Icons[i].Length)
            {
                Skills_LevelUp_Icons[i][level].texture = PlusIcon;
                Skills_LevelUp_Icons[i][level].color = LevelImprovedIconColor;
            }
        }
    }

    public void ClearAllSkillsLevelUps(int[] HeroLevelsOfSkills)
    {
        for (int i = 0; i < Skills_LevelUp_Icons.Length; i++)
        {
            for (int j = HeroLevelsOfSkills[i]; j < Skills_LevelUp_Icons[i].Length; j++)
            {
                Skills_LevelUp_Icons[i][j].texture = null;
                Skills_LevelUp_Icons[i][j].color = EmptyIconColor;
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
        Skills_LevelUp_Icons[SkillId][SkillLevelId].color = LevelImprovedIconColor;
    }

    public void SetSkillsPanels(SkillConfig[] SkillsData)
    {
        for (int i = 0; i < SkillsPanels.Length; i++)
        {
            SkillsPanels[i].SkillTexture = SkillsData[i].SkillIcon.texture;

            if (SkillsData[i].Type == SkillType.Passive)
            {
                SkillsPanels[i].SetKeyPanelActive(false);
            }
        }
    }

    private void OnHeroSelectWrapper(SkillConfig[] SkillsData, int[] LevelsOfSkills, HeroAttributes currentHeroAttributes,
        HeroInventory heroInventory)
    {
        if (currentHeroAttributes.Lv > 1)
        {
            ChangeSkillsLevelUp(LevelsOfSkills);
        }

        SetSkillsPanels(SkillsData);
    }

    private void OnLevelUpWrapper(int[] LevelsOfSkills, HeroAttributes heroAttributes)
    {
        if (heroAttributes.Lv > 1)
        {
            ChangeSkillsLevelUp(LevelsOfSkills);
        }
    }

    private void OnEnable()
    {
        HeroEvents.OnHeroSelectHendler += OnHeroSelectWrapper;

        HeroEvents.OnLevelUpHendler += OnLevelUpWrapper;

        HeroEvents.OnSkillLevelUpHendler += LevelUpSkill;
    }

    private void OnDisable()
    {
        HeroEvents.OnHeroSelectHendler -= OnHeroSelectWrapper;

        HeroEvents.OnLevelUpHendler -= OnLevelUpWrapper;

        HeroEvents.OnSkillLevelUpHendler -= LevelUpSkill;
    }
}
