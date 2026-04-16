public class EnemyEvents
{
    public delegate void OnEnemyAttack(EnemyEventArgs EnemyArgs);
    public static OnEnemyAttack OnEnemyAttackHandler;

    public delegate void OnEnemyDeath();
    public static OnEnemyDeath OnEnemyDeathHandler;
}
