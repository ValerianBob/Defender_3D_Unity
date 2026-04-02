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

    // Maybe change Skills arhitecture later :
    [SerializeField] private SkillSlot[] SkillSlots = new SkillSlot[MaxSkillsQuantity];

    public void Init(HeroController heroController)
    {
        _heroController = heroController;

        for (int i = 0; i < SkillSlots.Length; i++)
        {
            float Damage = SkillSlots[i].SkillData.BaseDamage;
            float CoolDown = SkillSlots[i].SkillData.BaseCoolDown;
            float ManaCost = SkillSlots[i].SkillData.BaseManaCost;

            SkillSlots[i].Skill.Damage = Damage;
            SkillSlots[i].Skill.CoolDown = CoolDown;
            SkillSlots[i].Skill.ManaCost = ManaCost;

            SkillSlots[i].Skill.CurrentSkillLevel = 0;
        }
    }

    public void ExecuteSkillById(int id)
    {
        SkillSlots[id].Skill.Execute();
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

            int[] heroLevelOfSkills = {
                GetSkillLevel(0),
                GetSkillLevel(1),
                GetSkillLevel(2),
                GetSkillLevel(3)
            };

            HeroEvents.OnSkillLevelUpHendler?.Invoke(SkillId, LevelOfSkill,
                _heroController.Hero_Attributes.PointsForLevelUpSckills,
                heroLevelOfSkills);

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

    private void OnEnable()
    {
        UIEvents.OnLevelUpUIHenlder += LevelUpSkillById;
    }

    private void OnDisable()
    {
        UIEvents.OnLevelUpUIHenlder -= LevelUpSkillById;
    }
}
