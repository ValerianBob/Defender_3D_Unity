using UnityEngine;

[System.Serializable]
public abstract class Skill : ScriptableObject, IShowInfo
{
    public string SkillName;
    public string SkillDescription;

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

        SkillName = config.SkillName;
        SkillDescription = config.Description;
    }

    public abstract void InitSkill(string name, string description, int CoolDown, int ManaCost, int SkillDuration);

    public abstract void Execute(HeroController CurrentHeroController);

    public abstract void UpdateSkill();

    public string GetTitle()
    {
        return SkillName;
    }

    public string GetDescription()
    {
        return $"{SkillDescription} \n" +
               $"CoolDown :{CoolDown} s \n" +
               $"ManaCost :{ManaCost} \n" +
               $"SkillDuration :{SkillDuration} s \n";
    }
}
