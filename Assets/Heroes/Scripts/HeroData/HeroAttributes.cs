[System.Serializable]
public class HeroAttributes
{
    public string HeroName;

    public float CurrentDamage;
    public float CurrentMagicDamage;

    public float CurrentAttackRange;

    public float CurrentMoveSpeed;
    public float CurrentAttackSpeed;

    public float MaxHealth;
    public float MaxMana;

    public float CurrentHealth;
    public float CurrentMana;

    public float HealthGain;
    public float ManaGain;

    public int Lv;
    public float CurrentXP;
    public float XPForLevelUP;

    public int PointsForLevelUpSckills;

    public HeroAttributes(HeroConfig heroConfig)
    {
        HeroName = heroConfig.heroName;

        CurrentDamage = heroConfig.attackDamage;
        CurrentMagicDamage = heroConfig.magicDamage;

        CurrentAttackRange = heroConfig.attackRange;

        CurrentMoveSpeed = heroConfig.moveSpeed;
        CurrentAttackSpeed = heroConfig.attackSpeed;

        CurrentHealth = heroConfig.maxHealth;
        CurrentMana = heroConfig.maxMana;

        MaxHealth = heroConfig.maxHealth;
        MaxMana = heroConfig.maxMana;

        HealthGain = heroConfig.healthGain;
        ManaGain = heroConfig.manaGain;

        Lv = heroConfig.lv;
        CurrentXP = heroConfig.currentXP;
        XPForLevelUP = heroConfig.xPForLevelUP;
    }
}
