using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISkills : MonoBehaviour
{
    [System.Serializable]
    private struct SkillPanel
    {
        [SerializeField] private RawImage skillIcon;
        [SerializeField] private GameObject keyPanel;
        [SerializeField] private TMP_Text coolDownText;
        [SerializeField] private InfoWindowTriggerUI infoWindowTrigger;

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

        public RawImage SkillIcon
        {
            get => skillIcon;
        }

        public TMP_Text CoolDownText
        {
            get => coolDownText;
        }

        public InfoWindowTriggerUI InfoWindowTrigger
        {
            get => infoWindowTrigger;
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

    private Color ReloadingColor = new Color(0.4f, 0.4f, 0.4f, 1f);
    private Color ReadyColor = new Color(1f, 1f, 1f, 1f);

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

    //private void SetSkillsPanels(HeroEventsArgs HeroArgs)
    //{
    //    for (int i = 0; i < SkillsPanels.Length; i++)
    //    {
    //        SkillsPanels[i].SkillTexture = HeroArgs.SkillsData[i].SkillIcon.texture;

    //        SkillsPanels[i].InfoWindowTrigger.Initialize(HeroArgs.Skills[i].Skill);

    //        if (HeroArgs.SkillsData[i].Type == SkillType.Passive || HeroArgs.SkillsData[i].Type == SkillType.OnAttackPassive)
    //        {
    //            SkillsPanels[i].SetKeyPanelActive(false);
    //        }
    //    }
    //}

    private void SetSkillsPanels(HeroEventsArgs HeroArgs)
    {
        for (int i = 0; i < SkillsPanels.Length; i++)
        {
            //SkillsPanels[i].SkillTexture = HeroArgs.SkillsData[i].SkillIcon.texture;
            SkillsPanels[i].SkillTexture = HeroArgs.Skills[i].SkillData.SkillIcon.texture;

            SkillsPanels[i].InfoWindowTrigger.Initialize(HeroArgs.Skills[i].Skill);

            if (HeroArgs.Skills[i].SkillData.Type == SkillType.Passive || HeroArgs.Skills[i].SkillData.Type == SkillType.OnAttackPassive)
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

    private void StartReloadSkill(HeroEventsArgs HeroArgs)
    {
        StartCoroutine(TimeToReloadSkill(HeroArgs.SkillId, HeroArgs.SkillCoolDown));
    }

    private IEnumerator TimeToReloadSkill(int SkillId, int CoolDown)
    {
        SkillsPanels[SkillId].CoolDownText.gameObject.SetActive(true);
        SkillsPanels[SkillId].SkillIcon.color = ReloadingColor;

        float count = CoolDown;

        while (count > 0)
        {
            SkillsPanels[SkillId].CoolDownText.text = count.ToString();

            yield return new WaitForSeconds(1f);
            
            count -= 1;
        }

        SkillsPanels[SkillId].CoolDownText.gameObject.SetActive(false);
        SkillsPanels[SkillId].SkillIcon.color = ReadyColor;
    }

    private void OnEnable()
    {
        HeroEvents.OnHeroSelectHandler += UpdateSkillsInfo;

        HeroEvents.OnLevelUpHandler += ChangeSkillsLevelUp;

        HeroEvents.OnSkillLevelUpHandler += LevelUpSkill;

        HeroEvents.OnSkillReloadHandler += StartReloadSkill;
    }

    private void OnDisable()
    {
        HeroEvents.OnHeroSelectHandler -= UpdateSkillsInfo;

        HeroEvents.OnLevelUpHandler -= ChangeSkillsLevelUp;

        HeroEvents.OnSkillLevelUpHandler -= LevelUpSkill;

        HeroEvents.OnSkillReloadHandler -= StartReloadSkill;
    }
}
