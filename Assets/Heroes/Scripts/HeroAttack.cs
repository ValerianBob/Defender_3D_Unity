using UnityEngine;

public class HeroAttack : MonoBehaviour
{
    private HeroController _heroController;

    private float NextTimeAttack = 0f;

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
            TryToAttack(target);
        }
    }

    private void TryToAttack(GameObject target)
    {
        if (Time.time >= NextTimeAttack)
        {
            NextTimeAttack = Time.time + _heroController.Hero_Attributes.CurrentAttackSpeed;

            _heroController.SetTrigger("Attack");

            Debug.Log("Hit target");
        }
    }
}
