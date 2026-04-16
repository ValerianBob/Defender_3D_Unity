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

    private void StopCauseDead()
    {
        _agent.ResetPath();
        _agent.isStopped = true;

        _agent.enabled = false;
    }

    private void Update()
    {
        if (!_enemyController.isDead)
        {
            if (_enemyController.IsInAttackRange())
            {
                _agent.updateRotation = false;

                RotateTowardTarget(_enemyController.CurrentTarget.transform.position);
            }
            else
            {
                _agent.updateRotation = true;
            }
        }
    }

    private void RotateTowardTarget(Vector3 target)
    {
        Vector3 direction = (target - transform.position);
        direction.y = 0f;

        if (direction == Vector3.zero)
        {
            return;
        }

        Quaternion targetRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(transform.rotation,targetRotation, Time.deltaTime * 10f);
    }

    private void OnDrawGizmosSelected()
    {
        if (_enemyController != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _enemyController.GetEnemyCurrentAttributes().CurrentDetectionDistance);
        }
    }

    private void OnEnable()
    {
        EnemyEvents.OnEnemyDeathHandler += StopCauseDead;
    }

    private void OnDisable()
    {
        EnemyEvents.OnEnemyDeathHandler -= StopCauseDead;
    }
}
