using UnityEngine;

public class EnemyAnimationsController : MonoBehaviour
{
    private EnemyController _enemyController;

    [SerializeField] private Animator _animator;

    public void Init(EnemyController CurrentEnemyController)
    {
        _enemyController = CurrentEnemyController;
    }

    private void Update()
    {
        if (_enemyController.GetNavMeshMagnitude() > 0.1f)
        {
            SetBool("IsWalking", true);

            ChangeWalkingSpeedAnimation();
        }
        else
        {
            SetBool("IsWalking", false);
        }
    }

    public void ChangeWalkingSpeedAnimation()
    {
        SetFloat("MoveSpeedMultiplier", _enemyController.GetEnemyCurrentAttributes().CurrentMoveSpeed / 5f);
    }

    public void PlayAttackAnimation(EnemyEventArgs EnemyArgs)
    {
        if (_enemyController == EnemyArgs.CurrentEnemyController)
        {
            SetTrigger("Attack");
            SetFloat("AttackSpeedMultiplier", 1f / _enemyController.GetEnemyCurrentAttributes().CurrentAttackSpeed);
        }
    }

    public void Play(string animationName)
    {
        _animator.Play(animationName);
    }

    public void SetBool(string paramName, bool value)
    {
        _animator.SetBool(paramName, value);
    }

    public void SetFloat(string paramName, float value)
    {
        _animator.SetFloat(paramName, value);
    }

    public void SetTrigger(string paramName)
    {
        _animator.SetTrigger(paramName);
    }

    public void SetAnimatorRootMotion()
    {
        _animator.applyRootMotion = true;
    }


    private void OnEnable()
    {
        EnemyEvents.OnEnemyAttackHandler += PlayAttackAnimation;
    }

    private void OnDisable()
    {
        EnemyEvents.OnEnemyAttackHandler -= PlayAttackAnimation;
    }
}
