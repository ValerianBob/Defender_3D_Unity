using UnityEngine;

[System.Serializable]
public abstract class Skill : ScriptableObject
{
    public int CoolDown;
    public int ManaCost;
    public int SkillDuration;

    public static readonly int MaxSkillLevel = 4;
    public int CurrentSkillLevel;

    public bool isReloading = false;

    protected Skill(SkillConfig config)
    {
        CoolDown = config.BaseCoolDown;
        ManaCost = config.BaseManaCost;
        SkillDuration = config.BaseSkillDuration;
    }

    public abstract void InitSkill(int CoolDown, int ManaCost, int SkillDuration);

    public abstract void Execute(HeroController CurrentHeroController);

    public abstract void UpdateSkill();
}
