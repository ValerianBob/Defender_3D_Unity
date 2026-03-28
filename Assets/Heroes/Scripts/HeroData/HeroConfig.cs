using UnityEngine;

[CreateAssetMenu(fileName = "HeroConfig", menuName = "Game/Hero Config")]
public class HeroConfig : ScriptableObject
{
    [SerializeField] private string HeroName;

    [SerializeField] private float AttackDamage;
    [SerializeField] private float MagicDamage;

    [SerializeField] private float AttackRange;

    [SerializeField] private float MoveSpeed;
    [SerializeField] private float AttackSpeed;

    [SerializeField] private float MaxHealth;
    [SerializeField] private float MaxMana;

    [SerializeField] private float HealthGain;
    [SerializeField] private float ManaGain;

    [SerializeField] private int Lv;
    [SerializeField] private float CurrentXP;
    [SerializeField] private float XPForLevelUP;

    public string heroName
    {
        get => HeroName;
    }

    public float attackDamage
    {
        get => AttackDamage;
    }

    public float magicDamage
    {
        get => MagicDamage;
    }

    public float attackRange
    {
        get => AttackRange;
    }

    public float moveSpeed
    {
        get => MoveSpeed;
    }

    public float attackSpeed
    {
        get => AttackSpeed;
    }

    public float maxHealth
    {
        get => MaxHealth;
    }

    public float maxMana
    {
        get => MaxMana;
    }

    public float healthGain
    {
        get => HealthGain;
    }

    public float manaGain
    {
        get => ManaGain;
    }

    public int lv
    {
        get => Lv;
    }

    public float currentXP
    {
        get => CurrentXP;
    }

    public float xPForLevelUP
    {
        get => XPForLevelUP;
    }
}
