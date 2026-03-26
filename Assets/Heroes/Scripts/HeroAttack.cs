using System.Collections;
using TMPro;
using UnityEngine;

public class HeroAttack : MonoBehaviour
{
    private HeroController _heroController;

    private float NextTimeAttack = 0f;

    public TMP_Text text;

    private void Start()
    {
        _heroController = GetComponent<HeroController>();
    }

    public void GetRangeToAttack(bool isInRange, GameObject target)
    {
        if (target == null)
        {
            return;
        }

        if (isInRange)
        {
            TryToAttack();
        }
    }

    private void TryToAttack()
    {
        if (Time.time >= NextTimeAttack)
        {
            float attackSpeed = _heroController.Hero_Attributes.CurrentAttackSpeed;
            NextTimeAttack = Time.time + attackSpeed;

            AnimationClip AttackClip = _heroController.GetAnimationClip("Attack");
            if (AttackClip != null)
            {
                _heroController.SetAnimatorSpeed(AttackClip.length / attackSpeed);
                StartCoroutine(ResetAnimatorSpeed());
            }

            _heroController.SetTrigger("Attack");
        }
    }

    private IEnumerator ResetAnimatorSpeed()
    {
        //Wait one frame :
        yield return null;

        //This loop runs every frame while the attack animation is playing :
        while (_heroController.GetAnimState("Attack"))
        {
            //Wait until next frame, then check again
            yield return null;
        }

        _heroController.SetAnimatorSpeed(1f);
    }

    //TODO change later :
    int count = 0;
    public void DealDamage()
    {
        count += 1;
        
        text.text = count.ToString();

        Debug.Log("Damage dealt!");
    }
}
