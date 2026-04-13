using System;

public static class HeroEvents
{
    public static Action OnAttack;

    public delegate void OnHeroAttack(HeroEventsArgs HeroArgs);
    public static OnHeroAttack OnHeroAttackHandler;

    public delegate void OnHeroSelect(HeroEventsArgs HeroArgs);
    public static OnHeroSelect OnHeroSelectHandler;

    public delegate void OnHealthChange(HeroEventsArgs HeroArgs);
    public static OnHealthChange OnHealthChangeHandler;

    public delegate void OnManaChange(HeroEventsArgs HeroArgs);
    public static OnManaChange OnManaChangeHandler;

    public delegate void OnXpGain(HeroEventsArgs HeroArgs);
    public static OnXpGain OnXpGainHandler;

    public delegate void OnLevelUp(HeroEventsArgs HeroArgs);
    public static OnLevelUp OnLevelUpHandler;

    public delegate void OnSkillLevelUp(HeroEventsArgs HeroArgs);
    public static OnSkillLevelUp OnSkillLevelUpHandler;

    public delegate void OnItemTake(HeroEventsArgs HeroArgs);
    public static OnItemTake OnItemTakeHandler;

    public delegate void OnItemSwap(HeroEventsArgs HeroArgs);
    public static OnItemSwap OnItemSwapHandler;

    public delegate void OnItemDrop(HeroEventsArgs HeroArgs);
    public static OnItemDrop OnItemDropHandler;

    public delegate void OnGoldGain(HeroEventsArgs HeroArgs);
    public static OnGoldGain OnGoldGainHandler;

    public delegate void OnSkillUse(HeroEventsArgs HeroArgs);
    public static OnSkillUse OnSkillUseHandler;

    public delegate void OnSkillReload(HeroEventsArgs HeroArgs);
    public static OnSkillReload OnSkillReloadHandler;
}
