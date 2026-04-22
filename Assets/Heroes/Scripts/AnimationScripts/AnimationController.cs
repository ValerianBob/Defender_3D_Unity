using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator CharacterAnimator;

    private HeroController _heroController;

    public void Init(HeroController heroController)
    {
        _heroController = heroController;
    }
    
    private void Update()
    {
        if (_heroController.isDead)
        {
            return;
        }

        if (_heroController.GetAgentMagnitude() > 0.1f)
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
        SetFloat("MoveSpeedMultiplier", _heroController.Hero_Attributes.CurrentMoveSpeed / 5f);
    }

    public void ChangeAttackSpeedAnimation()
    {
        SetTrigger("Attack");
        SetFloat("AttackSpeedMultiplier", 1f / _heroController.Hero_Attributes.CurrentAttackSpeed);
    }

    public void Play(string animationName)
    {
        CharacterAnimator.Play(animationName);
    }

    public void SetBool(string paramName, bool value)
    {
        CharacterAnimator.SetBool(paramName, value);
    }

    public void SetFloat(string paramName, float value)
    {
        CharacterAnimator.SetFloat(paramName, value);
    }

    public void SetTrigger(string paramName)
    {
        CharacterAnimator.SetTrigger(paramName);
    }

    public void ApplyRootMotion(bool isApplying)
    {
        if (isApplying)
        {
            CharacterAnimator.applyRootMotion = true;
        }
        else
        {
            CharacterAnimator.applyRootMotion = false;
        }
    }

    private void OnEnable()
    {
        HeroEvents.OnAttack += ChangeAttackSpeedAnimation;
    }

    private void OnDisable()
    {
        HeroEvents.OnAttack -= ChangeAttackSpeedAnimation;
    }
}