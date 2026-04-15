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
}
