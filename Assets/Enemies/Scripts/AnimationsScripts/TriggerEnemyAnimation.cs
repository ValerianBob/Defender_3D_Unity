using UnityEngine;

public class TriggerEnemyAnimation : MonoBehaviour
{
    [SerializeField] private EnemyAttack currentEnemyAttack;

    public void DealAttackEvent()
    {
        currentEnemyAttack.DealAttack();
    }
}
