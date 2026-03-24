using UnityEngine;
using UnityEngine.AI;

public class HeroController : MonoBehaviour
{
    [SerializeField] private Camera MainCamera;
    [SerializeField] private Animator CharacterAnimator;
    [SerializeField] private HeroConfig Hero_Config;

    private HeroMovement _heroMovement;
    private HeroAttack _heroAttack;

    public HeroAttributes Hero_Attributes;

    private NavMeshAgent _agent;

    private Ray Ray;

    public bool IsAttackingEnemy = false;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();

        _heroMovement = GetComponent<HeroMovement>();
        _heroAttack = GetComponent<HeroAttack>();

        Hero_Attributes = new HeroAttributes(Hero_Config);

        _agent.speed = Hero_Attributes.CurrentMoveSpeed;
    }

    private void Update()
    {
        Ray = MainCamera.ScreenPointToRay(InputReader.Instance.MousePosition);

        //TODO : Make HeroController tell what to do HeroMovement and HeroAttack
        if (InputReader.Instance.MouseRightClick)
        {
            if (Physics.Raycast(Ray, out RaycastHit hit, 100f))
            {
                if (hit.collider.gameObject.CompareTag("Environment"))
                {
                    Debug.Log("Hit Environment");
                }
                else if (hit.collider.gameObject.CompareTag("Floor"))
                {
                    _heroMovement.MoveToPosition(hit.point);
                }
                else if (hit.collider.gameObject.CompareTag("Enemy"))
                {
                    Debug.Log("Hit Enemy");
                }
            }
        }
    }
    
    public float GetAgentMagnitude()
    {
        return _agent.velocity.magnitude;
    }

    public void SetAgentDestination(Vector3 point)
    {
        _agent.SetDestination(point);
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

    public Vector3 GetRayOrigin()
    {
        return Ray.origin;
    }

    public Vector3 GetRayDirection()
    {
        return Ray.direction;
    }

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
