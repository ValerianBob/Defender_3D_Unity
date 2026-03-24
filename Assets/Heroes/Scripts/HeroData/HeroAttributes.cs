public class HeroAttributes
{
    public float CurrentDamage;
    public float CurrentMagicDamage;

    public float CurrentAttackRange;

    public float CurrentMoveSpeed;
    public float CurrentAttackSpeed;

    public float CurrentHealth;
    public float CurrentMana;

    public HeroAttributes(HeroConfig heroConfig)
    {
        CurrentDamage = heroConfig.AttackDamage;
        CurrentMagicDamage = heroConfig.MagicDamage;

        CurrentAttackRange = heroConfig.AttackRange;

        CurrentMoveSpeed = heroConfig.MoveSpeed;
        CurrentAttackSpeed = heroConfig.AttackSpeed;

        CurrentHealth = heroConfig.MaxHealth;
        CurrentMana = heroConfig.MaxMana;
    }
}
