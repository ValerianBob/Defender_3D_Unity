using UnityEngine;

public class HeroSkills : MonoBehaviour
{
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

    private void Start()
    {
        for (int i = 0; i < SkillSlots.Length; i++)
        {
            float Damage = SkillSlots[i].SkillData.BaseDamage;
            float CoolDown = SkillSlots[i].SkillData.BaseCoolDown;
            float ManaCost = SkillSlots[i].SkillData.BaseManaCost;

            SkillSlots[i].Skill.Damage = Damage;
            SkillSlots[i].Skill.CoolDown = CoolDown;
            SkillSlots[i].Skill.ManaCost = ManaCost;

            SkillSlots[i].Skill.CurrentSkillLevel = 1;
        }
    }

    public void ExecuteSkillById(int id)
    {
        SkillSlots[id].Skill.Execute();
    }

    public void LevelUpSkillById(int id)
    {
        if (SkillSlots[id].Skill.CurrentSkillLevel == Skill.MaxSkillLevel)
        {
            return;
        }

        SkillSlots[id].Skill.CurrentSkillLevel += 1;
    }

    public int GetSkillLevel(int id)
    {
        return SkillSlots[id].Skill.CurrentSkillLevel;
    }
}
