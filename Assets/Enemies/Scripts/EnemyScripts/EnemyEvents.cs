public class EnemyEvents
{
    public delegate void OnEnemyAttack(EnemyEventArgs EnemyArgs);
    public static OnEnemyAttack OnEnemyAttackHandler;

    public delegate void OnEnemyDeath(EnemyEventArgs EnemyArgs);
    public static OnEnemyDeath OnEnemyDeathHandler;
}
