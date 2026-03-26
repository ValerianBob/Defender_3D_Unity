using UnityEngine;
using UnityEngine.AI;

public class HeroController : MonoBehaviour
{
    [SerializeField] private Camera MainCamera;
    [SerializeField] private Animator CharacterAnimator;
    [SerializeField] private HeroConfig Hero_Config;

    public HeroAttributes Hero_Attributes;

    private NavMeshAgent _agent;

    private HeroMovement _heroMovement;
    private HeroAttack _heroAttack;

    private Ray Ray;
    private RaycastHit _hit;

    public GameObject CurrentTarget;

    private void Start()
    {
        Hero_Attributes = new HeroAttributes(Hero_Config);

        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.speed = Hero_Attributes.CurrentMoveSpeed;

        _heroMovement = GetComponent<HeroMovement>();
        _heroAttack = GetComponent<HeroAttack>();
    }

    private void Update()
    {
        Ray = MainCamera.ScreenPointToRay(InputReader.Instance.MousePosition);

        if (InputReader.Instance.MouseRightClick)
        {
            if (Physics.Raycast(Ray, out _hit, 100f))
            {
                if (_hit.collider.gameObject.CompareTag("Environment"))
                {
                    Debug.Log("Hit Environment");
                }
                else if (_hit.collider.gameObject.CompareTag("Floor"))
                {
                    Debug.Log("Go to position");

                    CurrentTarget = null;

                    _heroMovement.Move(_hit.point);
                }
                else if (_hit.collider.gameObject.CompareTag("Enemy"))
                {
                    Debug.Log("Pursuing Enemy");

                    CurrentTarget = _hit.collider.gameObject;
                }
            }
        }

        if (CurrentTarget != null)
        {
            bool isInRange = _heroMovement.IsInRange(CurrentTarget.transform.position, Hero_Attributes.CurrentAttackRange);

            _heroAttack.GetRangeToAttack(isInRange, CurrentTarget);
        }
    }
    
    // NavMeshAgent :
    public float GetAgentMagnitude()
    {
        return _agent.velocity.magnitude;
    }

    public void SetAgentDestination(Vector3 point)
    {
        _agent.SetDestination(point);
    }

    public bool GetIsAgentStop()
    {
        return _agent.isStopped;
    }

    public void SetAgentStop(bool stopAgent)
    {
        if (stopAgent)
        {
            _agent.isStopped = true;
        }
        else
        {
            _agent.isStopped = false;
        }
    }

    public void ResetAgentPath()
    {
        _agent.ResetPath();
    }

    // Animations :
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

    public void SetAnimatorSpeed(float speed)
    {
        CharacterAnimator.speed = speed;
    }

    public AnimationClip GetAnimationClip(string ClipName)
    {
        AnimationClip AnimClip = null;

        foreach (var clip in CharacterAnimator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == ClipName)
            {
                AnimClip = clip;
                break;
            }
        }

        return AnimClip;
    }

    public bool GetAnimState(string name)
    {
        return CharacterAnimator.GetCurrentAnimatorStateInfo(0).IsName(name);
    }

    // Ray :
    public Vector3 GetRayOrigin()
    {
        return Ray.origin;
    }

    public Vector3 GetRayDirection()
    {
        return Ray.direction;
    }

    public Vector3 GetRayHitPoint()
    {
        return _hit.point;
    }

    // Rotation :
    public void ChangeAgentRotationManualy(bool manualy)
    {
        if (manualy)
        {
            _agent.updateRotation = false;
        }
        else
        {
            _agent.updateRotation = true;
        }
    }
}
