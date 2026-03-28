using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class HeroController : MonoBehaviour
{
    [SerializeField] private Camera MainCamera;
    [SerializeField] private Animator CharacterAnimator;
    [SerializeField] private HeroConfig Hero_Config;
    private HeroMovement _heroMovement;
    private HeroAttack _heroAttack;

    private Ray Ray;
    private RaycastHit _hit;

    public HeroAttributes Hero_Attributes;

    private NavMeshAgent _agent;

    public GameObject CurrentTarget;

    private void Awake()
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
                    CurrentTarget = null;

                    _agent.speed = Hero_Attributes.CurrentMoveSpeed;

                    _heroMovement.Move(_hit.point);
                }
                else if (_hit.collider.gameObject.CompareTag("Enemy"))
                {
                    CurrentTarget = _hit.collider.gameObject;
                }
            }
        }

        if (CurrentTarget != null)
        {
            bool isInRange = _heroMovement.IsInRange(CurrentTarget.transform.position, Hero_Attributes.CurrentAttackRange);

            _heroAttack.GetRangeToAttack(isInRange, CurrentTarget);
        }

        //Test delete later :
        if (Keyboard.current.dKey.wasPressedThisFrame)
        {
            ChangeHealth(true, 1f);
        }
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            ChangeHealth(false, 1f);
        }

        if (Keyboard.current.xKey.wasPressedThisFrame)
        {
            ChagneMana(false, 1f);
        }
        if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            ChagneMana(true, 1f);
        }

        if (Keyboard.current.vKey.wasPressedThisFrame)
        {
            GainXp(1f);
        }
    }
    
    public void ChangeHealth(bool isGettingDamage, float damage)
    {
        if (isGettingDamage)
        {
            Hero_Attributes.CurrentHealth -= damage;
        }
        else
        {
            Hero_Attributes.CurrentHealth += damage;
        }

        if (Hero_Attributes.CurrentHealth < 0)
        {
            Hero_Attributes.CurrentHealth = 0;
        }
        else if (Hero_Attributes.CurrentHealth > Hero_Attributes.MaxHealth)
        {
            Hero_Attributes.CurrentHealth = Hero_Attributes.MaxHealth;
        }

        HeroEvents.OnHealthChange?.Invoke(Hero_Attributes.CurrentHealth, Hero_Attributes.MaxHealth);
    }

    public void ChagneMana(bool isGettingMana, float magaAmount)
    {
        if (isGettingMana)
        {
            Hero_Attributes.CurrentMana += magaAmount;
        }
        else
        {
            Hero_Attributes.CurrentMana -= magaAmount;
        }

        if (Hero_Attributes.CurrentMana < 0)
        {
            Hero_Attributes.CurrentMana = 0;
        }
        else if (Hero_Attributes.CurrentMana > Hero_Attributes.MaxMana)
        {
            Hero_Attributes.CurrentMana = Hero_Attributes.MaxMana;
        }

        HeroEvents.OnManaChange?.Invoke(Hero_Attributes.CurrentMana, Hero_Attributes.MaxMana);
    }

    //TODO make that GainXp Up level and other stuff:
    public void GainXp(float XpAmount)
    {
        Hero_Attributes.CurrentXP += XpAmount;

        HeroEvents.OnXpGain?.Invoke(Hero_Attributes.CurrentXP, Hero_Attributes.XPForLevelUP);
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

    //public void SetAnimatorSpeed(float speed)
    //{
    //    CharacterAnimator.speed = speed;
    //}

    //public AnimationClip GetAnimationClip(string ClipName)
    //{
    //    AnimationClip AnimClip = null;

    //    foreach (var clip in CharacterAnimator.runtimeAnimatorController.animationClips)
    //    {
    //        if (clip.name == ClipName)
    //        {
    //            AnimClip = clip;
    //            break;
    //        }
    //    }

    //    return AnimClip;
    //}

    //public bool GetAnimState(string name)
    //{
    //    return CharacterAnimator.GetCurrentAnimatorStateInfo(0).IsName(name);
    //}

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
    //public void ChangeAgentRotationManualy(bool manualy)
    //{
    //    if (manualy)
    //    {
    //        _agent.updateRotation = false;
    //    }
    //    else
    //    {
    //        _agent.updateRotation = true;
    //    }
    //}
}
