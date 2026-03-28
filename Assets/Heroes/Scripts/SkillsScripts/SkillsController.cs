using UnityEngine;

public class SkillsController : MonoBehaviour
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
        }
    }

    private void Update()
    { 
        if (InputReader.Instance.QButton)
        {
            SkillSlots[0].Skill.Execute();
        }
        if (InputReader.Instance.WButton)
        {
            SkillSlots[1].Skill.Execute();
        }
        if (InputReader.Instance.EButton)
        {
            SkillSlots[2].Skill.Execute();
        }
        if (InputReader.Instance.RButton)
        {
            SkillSlots[3].Skill.Execute();
        }
    }
}
