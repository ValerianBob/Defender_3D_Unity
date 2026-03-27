using UnityEngine;

public enum SkillType
{
    Active,
    Passive
}

[CreateAssetMenu(fileName = "NewSkill", menuName ="Hero/Skill")]
public class SkillConfig : ScriptableObject
{
    public string SkillName { get; private set; }
    public SkillType Type { get; private set; }
    public Sprite SkillIcon { get; private set; }

    public float BaseCoolDown { get; private set; }
    public float BaseManaCost { get; private set; }
    public float BaseDamage { get; private set; }
}
