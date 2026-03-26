using UnityEngine;

public class HeroMovement : MonoBehaviour
{
    private HeroController _heroController;

    [SerializeField] private GameObject Marker;

    private float RotationSpeed = 20f;

    private void Start()
    {
        _heroController = GetComponent<HeroController>();
    }

    private void Update()
    {
        if (_heroController.CurrentTarget != null)
        {
            PursuingTarget(_heroController.CurrentTarget.transform.position, _heroController.Hero_Attributes.CurrentAttackRange);

            RotateCharacter(_heroController.CurrentTarget.transform.position);

            return;
        }

        RotateCharacter(_heroController.GetRayHitPoint());
    }

    public void PursuingTarget(Vector3 target, float attackRange)
    {
        if (target == null)
        {
            return;
        }

        float distance = Vector3.Distance(transform.position, target);

        if (distance > attackRange)
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
        if (_heroController.GetIsAgentStop())
        {
            _heroController.SetAgentStop(false);
        }

        if (_heroController.CurrentTarget == null)
        {
            Instantiate(Marker, target, Marker.transform.rotation);
        }

        _heroController.SetAgentDestination(target);
    }

    public void Stop()
    {
        if (!_heroController.GetIsAgentStop())
        {
            _heroController.ResetAgentPath();
            _heroController.SetAgentStop(true);
        }
    }

    public void RotateCharacter(Vector3 position)
    {
        Vector3 direction = (position - transform.position).normalized;
        direction.y = 0f;

        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(transform.rotation,lookRotation, RotationSpeed * Time.deltaTime);
        }
    }

    public bool IsInRange(Vector3 target, float range)
    {
        float distance = Vector3.Distance(transform.position, target);
        
        if (distance > range)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
