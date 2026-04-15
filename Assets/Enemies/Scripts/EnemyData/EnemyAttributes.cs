[System.Serializable]
public class EnemyAttributes
{
    public EnemyType EnemyType { get; private set; }

    public string EnemyName { get; private set; }

    public int CurrentAttackDamage;

    public int CurrentAttackRange;

    public float CurrentAttackSpeed;

    public int CurrentMoveSpeed;

    public int CurrentMaxHealth;
    public int CurrentHealth;

    public int CurrentHealthGain;

    public int CurrentLevel;

    public float CurrentDetectionDistance;

    public EnemyAttributes(EnemyConfig BaseEnemyData)
    {
        EnemyType = BaseEnemyData.EnemyType;
        EnemyName = BaseEnemyData.EnemyName;

        CurrentAttackDamage = BaseEnemyData.AttackDamage;
        CurrentAttackRange = BaseEnemyData.AttackRange;
        CurrentAttackSpeed = BaseEnemyData.AttackSpeed;

        CurrentMoveSpeed = BaseEnemyData.MoveSpeed;

        CurrentMaxHealth = BaseEnemyData.MaxHealth;
        CurrentHealth = BaseEnemyData.MaxHealth;
        CurrentHealthGain = BaseEnemyData.HealthGain;

        CurrentLevel = BaseEnemyData.Level;

        CurrentDetectionDistance = BaseEnemyData.DetectionDistance;
    }

}
