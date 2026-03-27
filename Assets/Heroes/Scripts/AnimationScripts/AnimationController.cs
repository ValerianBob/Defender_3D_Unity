using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private HeroController _heroController;

    private void Start()
    {
        _heroController = GetComponent<HeroController>();
    }

    private void Update()
    {
        if (_heroController.GetAgentMagnitude() > 0.1f)
        {
            _heroController.SetBool("IsWalking", true);

            ChangeWalkingSpeedAnimation();
        }
        else
        {
            _heroController.SetBool("IsWalking", false);
        }
    }

    public void ChangeWalkingSpeedAnimation()
    {
        _heroController.SetFloat("MoveSpeedMultiplier", _heroController.Hero_Attributes.CurrentMoveSpeed / 5f);
    }

    public void ChangeAttackSpeedAnimation()
    {
        _heroController.SetTrigger("Attack");
        _heroController.SetFloat("AttackSpeedMultiplier", 1f / _heroController.Hero_Attributes.CurrentAttackSpeed);
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