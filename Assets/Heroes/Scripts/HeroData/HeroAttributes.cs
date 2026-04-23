[System.Serializable]
public class HeroAttributes
{
    public string HeroName;

    public int CurrentDamage;
    public int CurrentMagicDamage;

    public int CurrentAttackRange;

    public int CurrentMoveSpeed;
    public float CurrentAttackSpeed;

    public int MaxHealth;
    public int MaxMana;

    public int CurrentHealth;
    public int CurrentMana;

    public int HealthGain;
    public int ManaGain;

    public int Lv;
    public float CurrentXP;
    public float XPForLevelUP;

    public int PointsForLevelUpSckills;

    public int RespawnTime;

    public int BuyBackCost;

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

        PointsForLevelUpSckills = 0;

        RespawnTime = 5;

        BuyBackCost = 100;
    }
}
