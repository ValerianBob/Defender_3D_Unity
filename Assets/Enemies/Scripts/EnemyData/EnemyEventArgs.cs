public class EnemyEventArgs
{
    public EnemyController CurrentEnemyController { get; private set; }

    public EnemyEventArgs(EnemyController currentEnemyController)
    {
        CurrentEnemyController = currentEnemyController;
    }
}
