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

    private void ChangeSkillsLevelUp(HeroEventsArgs HeroArgs)
    {
        for (int i = 0; i < Skills_LevelUp_Icons.Length; i++)
        {
            int level = HeroArgs.LevelsOfSkills[i];

            if (level < Skills_LevelUp_Icons[i].Length)
            {
                Skills_LevelUp_Icons[i][level].texture = PlusIcon;
                Skills_LevelUp_Icons[i][level].color = LevelImprovedIconColor;
            }
        }
    }

    private void ClearAllSkillsLevelUps(HeroEventsArgs HeroArgs)
    {
        for (int i = 0; i < Skills_LevelUp_Icons.Length; i++)
        {
            for (int j = HeroArgs.LevelsOfSkills[i]; j < Skills_LevelUp_Icons[i].Length; j++)
            {
                Skills_LevelUp_Icons[i][j].texture = null;
                Skills_LevelUp_Icons[i][j].color = EmptyIconColor;
            }
        }
    }

    private void LevelUpSkill(HeroEventsArgs HeroArgs)
    {
        if (HeroArgs.CurrentHeroAttributes.PointsForLevelUpSckills > 0 && HeroArgs.CurrentHeroAttributes.Lv > 1)
        {
            ChangeSkillsLevelUp(HeroArgs);
        }
        else
        {
            ClearAllSkillsLevelUps(HeroArgs);
        }

        Skills_LevelUp_Icons[HeroArgs.SkillId][HeroArgs.SkillLevelId].texture = null;
        Skills_LevelUp_Icons[HeroArgs.SkillId][HeroArgs.SkillLevelId].color = LevelImprovedIconColor;
    }

    private void SetSkillsPanels(HeroEventsArgs HeroArgs)
    {
        for (int i = 0; i < SkillsPanels.Length; i++)
        {
            SkillsPanels[i].SkillTexture = HeroArgs.SkillsData[i].SkillIcon.texture;

            if (HeroArgs.SkillsData[i].Type == SkillType.Passive)
            {
                SkillsPanels[i].SetKeyPanelActive(false);
            }
        }
    }

    private void UpdateSkillsInfo(HeroEventsArgs HeroArgs)
    {
        if (HeroArgs.CurrentHeroAttributes.Lv > 1)
        {
            ChangeSkillsLevelUp(HeroArgs);
        }

        SetSkillsPanels(HeroArgs);
    }

    private void OnEnable()
    {
        HeroEvents.OnHeroSelectHandler += UpdateSkillsInfo;

        HeroEvents.OnLevelUpHandler += ChangeSkillsLevelUp;

        HeroEvents.OnSkillLevelUpHandler += LevelUpSkill;
    }

    private void OnDisable()
    {
        HeroEvents.OnHeroSelectHandler -= UpdateSkillsInfo;

        HeroEvents.OnLevelUpHandler -= ChangeSkillsLevelUp;

        HeroEvents.OnSkillLevelUpHandler -= LevelUpSkill;
    }
}
