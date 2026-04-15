using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private EnemyController _enemyController;

    public NavMeshAgent _agent {  get; private set; }

    public void Init(EnemyController CurrentEnemyController)
    {
        _enemyController = CurrentEnemyController;

        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = CurrentEnemyController.GetEnemyCurrentAttributes().CurrentMoveSpeed;
    }


    public void PursuingTarget(Vector3 target)
    {
        float distance = Vector3.Distance(transform.position, target);

        if (distance > _enemyController.GetEnemyCurrentAttributes().CurrentAttackRange)
        {
            Move(target);
        }
        else
        {
            Stop();
        }
    }

    public void Move(Vector3 target)
    {
        _agent.isStopped = false;
        _agent.SetDestination(target);
    }

    public void Stop()
    {
        _agent.ResetPath();
        _agent.isStopped = true;
    }

    private void OnDrawGizmosSelected()
    {
        if (_enemyController != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _enemyController.GetEnemyCurrentAttributes().CurrentDetectionDistance);
        }
    }
}
