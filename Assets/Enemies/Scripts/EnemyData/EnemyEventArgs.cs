using UnityEngine;

public class EnemyEventArgs
{
    public GameObject CurrentEnemyObject {  get; private set; }

    public EnemyController CurrentEnemyController { get; private set; }

    public EnemyEventArgs(GameObject currentEnemyObject)
    {
        CurrentEnemyObject = currentEnemyObject;
    }

    public EnemyEventArgs(EnemyController currentEnemyController)
    {
        CurrentEnemyController = currentEnemyController;
    }

    public EnemyEventArgs(EnemyController currentEnemyController, GameObject currentEnemyObject)
    {
        CurrentEnemyObject = currentEnemyObject;
        CurrentEnemyController = currentEnemyController;
    }
}
