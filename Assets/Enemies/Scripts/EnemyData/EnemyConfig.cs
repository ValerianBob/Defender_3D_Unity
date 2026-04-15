using UnityEngine;

public enum EnemyType
{
    BaseRaider,
    Neutral
}
[CreateAssetMenu(fileName = "EnemyConfig", menuName = "Enemy/EnemyConfig")]
public class EnemyConfig : ScriptableObject
{
    [SerializeField] private EnemyType enemyType;

    [SerializeField] private string enemyName;

    [SerializeField] private int attackDamage;

    [SerializeField] private int attackRange;

    [SerializeField] private float attackSpeed;

    [SerializeField] private int moveSpeed;

    [SerializeField] private int maxHealth;

    [SerializeField] private int healthGain;

    [SerializeField] private int level;

    [SerializeField] private float detectionDistance;

    public EnemyType EnemyType
    {
        get => enemyType;
    }

    public string EnemyName
    {
        get => enemyName;
    }

    public int AttackDamage
    {
        get => attackDamage;
    }

    public int AttackRange
    {
        get => attackRange;
    }

    public float AttackSpeed
    {
        get => attackSpeed;
    }

    public int MoveSpeed
    {
        get => moveSpeed;
    }

    public int MaxHealth
    {
        get => maxHealth;
    }

    public int HealthGain
    {
        get => healthGain;
    }

    public int Level
    {
        get => level;
    }

    public float DetectionDistance
    {
        get => detectionDistance;
    }
}
