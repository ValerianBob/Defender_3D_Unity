using System;

public static class HeroEvents
{
    public static Action OnAttack;

    public delegate void OnHeroSpawn(SkillConfig[] SkillsData);
    public static OnHeroSpawn OnHeroSpawnHendler;

    public delegate void OnHealthChange(float CurrentHealth, float MaxHealth);
    public static OnHealthChange OnHealthChangeHenlder;

    public delegate void OnManaChange(float CurrentMana, float MaxMana);
    public static OnManaChange OnManaChangeHendler;

    public delegate void OnXpGain(float CurrentXP, float XpForLevelUp);
    public static OnXpGain OnXpGainHendler;

    public delegate void OnLevelUp(int[] HeroLevelOfSkills, HeroAttributes HeroCurrentAttributes);
    public static OnLevelUp OnLevelUpHendler;

    public delegate void OnSkillLevelUp(int SkillId, int LevelOfSkill, int PointsForLevelUpSkill, int[] HeroLevelOfSkills);
    public static OnSkillLevelUp OnSkillLevelUpHendler;
}
