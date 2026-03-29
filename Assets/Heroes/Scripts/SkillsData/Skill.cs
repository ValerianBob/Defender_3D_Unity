using UnityEngine;

[System.Serializable]
public abstract class Skill : ScriptableObject
{
    public float CoolDown;
    public float ManaCost;
    public float Damage;

    public static readonly int MaxSkillLevel = 4;
    public int CurrentSkillLevel;

    protected Skill(SkillConfig config)
    {
        CoolDown = config.BaseCoolDown;
        ManaCost = config.BaseManaCost;
        Damage = config.BaseDamage;
    }

    public abstract void Execute();
}
