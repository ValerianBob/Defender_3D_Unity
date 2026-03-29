using System;

public static class HeroEvents
{
    public static Action OnAttack;
    public static Action<float, float> OnHealthChange;
    public static Action<float, float> OnManaChange;
    public static Action<float, float> OnXpGain;
    public static Action<int, int, int, int, int> OnLevelUp;
}
