using System;
using UnityEngine;

[CreateAssetMenu(fileName = "HeroConfig", menuName = "Game/Hero Config")]
public class HeroConfig : ScriptableObject
{
    [SerializeField] private string HeroName;

    [SerializeField] private int AttackDamage;
    [SerializeField] private int MagicDamage;

    [SerializeField] private int AttackRange;

    [SerializeField] private int MoveSpeed;
    [SerializeField] private float AttackSpeed;

    [SerializeField] private int MaxHealth;
    [SerializeField] private int MaxMana;

    [SerializeField] private int HealthGain;
    [SerializeField] private int ManaGain;

    [SerializeField] private int Lv;
    [SerializeField] private float CurrentXP;
    [SerializeField] private float XPForLevelUP;

    [SerializeField] private SoundData attackSounds;

    public string heroName
    {
        get => HeroName;
    }

    public int attackDamage
    {
        get => AttackDamage;
    }

    public int magicDamage
    {
        get => MagicDamage;
    }

    public int attackRange
    {
        get => AttackRange;
    }

    public int moveSpeed
    {
        get => MoveSpeed;
    }

    public float attackSpeed
    {
        get => AttackSpeed;
    }

    public int maxHealth
    {
        get => MaxHealth;
    }

    public int maxMana
    {
        get => MaxMana;
    }

    public int healthGain
    {
        get => HealthGain;
    }

    public int manaGain
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

    public SoundData AttackSounds
    {
        get => attackSounds;
    }
}
