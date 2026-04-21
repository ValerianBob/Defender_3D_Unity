using System.Collections;
using UnityEngine;

public class HeroSkills : MonoBehaviour
{
    private HeroController _heroController;

    private const int MaxSkillsQuantity = 4;

    [System.Serializable]
    public struct SkillSlot
    {
        [SerializeField] private SkillConfig skillData;  // Editable in Inspector
        public Skill Skill;                              // Runtime skill instance

        // Public read-only access to SkillData
        public SkillConfig SkillData => skillData;
    }

    [SerializeField] private SkillSlot[] SkillSlots = new SkillSlot[MaxSkillsQuantity];

    public void Init(HeroController heroController)
    {
        _heroController = heroController;

        for (int i = 0; i < SkillSlots.Length; i++)
        {
            int CoolDown = SkillSlots[i].SkillData.BaseCoolDown;
            int ManaCost = SkillSlots[i].SkillData.BaseManaCost;
            int SkillDuration = SkillSlots[i].SkillData.BaseSkillDuration;

            SkillSlots[i].Skill.InitSkill(CoolDown, ManaCost, SkillDuration);
        }
    }

    public void ExecuteSkillById(int id, HeroController CurrentHeroController)
    {
        if (SkillSlots[id].Skill.ManaCost > CurrentHeroController.Hero_Attributes.CurrentMana)
        {
            Debug.Log($"Not enough mana");

            return;
        }

        if (SkillSlots[id].SkillData.Type == SkillType.Passive)
        {
            SkillSlots[id].Skill.Execute(CurrentHeroController);
        }
        else
        {
            if (!SkillSlots[id].Skill.isReloading)
            {
                SkillSlots[id].Skill.Execute(CurrentHeroController);
                SkillSlots[id].Skill.isReloading = true;
                StartCoroutine(ReloadSkillById(id));
            }
            else
            {
                Debug.Log($"Skill :{SkillSlots[id].SkillData.SkillName} is reloading");
            }
        }
    }

    public void LevelUpSkillById(int SkillId, int LevelOfSkill)
    {
        if (SkillSlots[SkillId].Skill.CurrentSkillLevel == Skill.MaxSkillLevel 
            || _heroController.Hero_Attributes.PointsForLevelUpSckills == 0)
        {
            Debug.Log("Can't improve skill because Points For Level Up Skill = 0 or Skill has Max Level");

            return;
        }

        if (SkillSlots[SkillId].Skill.CurrentSkillLevel == LevelOfSkill)
        {
            SkillSlots[SkillId].Skill.CurrentSkillLevel += 1;

            _heroController.Hero_Attributes.PointsForLevelUpSckills -= 1;

            if (SkillSlots[SkillId].Skill.CurrentSkillLevel > 1)
            {
                SkillSlots[SkillId].Skill.UpdateSkill();
            }

            HeroEventsArgs HeroArgs = new HeroEventsArgs(SkillId, LevelOfSkill, GetAllSkillsData(), _heroController.Hero_Attributes,
                _heroController.HeroInventory, GetAllSkillsLevels());

            HeroEvents.OnSkillLevelUpHandler?.Invoke(HeroArgs);

            Debug.Log($"Skill :{SkillSlots[SkillId].SkillData.SkillName} was improved to level : {SkillSlots[SkillId].Skill.CurrentSkillLevel}");
        }
        else
        {
            Debug.Log($"Can't improve skill on level :{LevelOfSkill + 1} because current level : {SkillSlots[SkillId].Skill.CurrentSkillLevel}");
        }
    }

    public int GetSkillLevel(int id)
    {
        return SkillSlots[id].Skill.CurrentSkillLevel;
    }

    public int[] GetAllSkillsLevels()
    {
        int[] heroLevelOfSkills = {
            GetSkillLevel(0), 
            GetSkillLevel(1), 
            GetSkillLevel(2), 
            GetSkillLevel(3)
        };

        return heroLevelOfSkills;
    }

    public SkillConfig[] GetAllSkillsData()
    {
        SkillConfig[] SkillDataToSend = new SkillConfig[MaxSkillsQuantity];

        for (int i = 0; i < MaxSkillsQuantity; i++)
        {
            SkillDataToSend[i] = SkillSlots[i].SkillData;
        }

        return SkillDataToSend;
    }

    public SkillType GetSkillTypeById(int id)
    {
        return SkillSlots[id].SkillData.Type;
    }

    private IEnumerator ReloadSkillById(int id)
    {
        HeroEventsArgs HeroArgs = new HeroEventsArgs(id, SkillSlots[id].Skill.CoolDown);
        HeroEvents.OnSkillReloadHandler?.Invoke(HeroArgs);

        yield return new WaitForSeconds(SkillSlots[id].Skill.CoolDown);
        SkillSlots[id].Skill.isReloading = false;
    }

    private void OnEnable()
    {
        UIEvents.OnLevelUpUIHandler += LevelUpSkillById;
    }

    private void OnDisable()
    {
        UIEvents.OnLevelUpUIHandler -= LevelUpSkillById;
    }
}
