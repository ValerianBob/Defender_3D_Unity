using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private EnemyController _enemyController;

    private float _nextAttackTime;

    private bool _isInAttackRange = false;

    public void Init(EnemyController CurrentEnemyController)
    {
        _enemyController = CurrentEnemyController;
    }

    private void Update()
    {
        _isInAttackRange = _enemyController.IsInAttackRange();

        if (_isInAttackRange)
        {
            TryToAttack();
        }
    }

    private void TryToAttack()
    {
        if (_enemyController.CurrentTarget != null)
        {
            if (Time.time >= _nextAttackTime)
            {
                _nextAttackTime = _enemyController.GetEnemyCurrentAttributes().CurrentAttackSpeed + Time.time;

                EnemyEventArgs EnemyArgs = new EnemyEventArgs(_enemyController);
                EnemyEvents.OnEnemyAttackHandler?.Invoke(EnemyArgs);
            }
        }
    }

    public void DealAttack()
    {
        if (_enemyController.CurrentTarget != null)
        {
            HeroController CurrentHero = _enemyController.CurrentTarget.GetComponent<HeroController>();

            if (CurrentHero != null)
            {
                CurrentHero.ChangeHealth(true, _enemyController.GetEnemyCurrentAttributes().CurrentAttackDamage);
            }
        }
    }
}
