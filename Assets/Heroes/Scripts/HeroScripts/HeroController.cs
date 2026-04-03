using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class HeroController : MonoBehaviour
{
    [SerializeField] private Camera MainCamera;
    [SerializeField] private Animator CharacterAnimator;
    [SerializeField] private HeroConfig Hero_Config;

    [SerializeField] private GameObject Marker;

    private HeroMovement _heroMovement;
    private HeroAttack _heroAttack;
    private HeroSkills _heroSkills;
    private HeroInventory _heroInventory;

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
        _heroSkills = GetComponent<HeroSkills>();
        _heroInventory = GetComponent<HeroInventory>();

        _heroMovement.Init(this);
        _heroAttack.Init(this);
        _heroSkills.Init(this);
        _heroInventory.Init(this);
    }

    private void Start()
    {
        // Maybe change later :

        //Update UI
        int[] heroLevelOfSkills = {
            _heroSkills.GetSkillLevel(0),
            _heroSkills.GetSkillLevel(1),
            _heroSkills.GetSkillLevel(2),
            _heroSkills.GetSkillLevel(3)
        };

        HeroEvents.OnHeroSelectHendler?.Invoke(_heroSkills.GetAllSkillsData(), heroLevelOfSkills, Hero_Attributes, _heroInventory);

        //HeroEvents.OnLevelUpHendler?.Invoke(heroLevelOfSkills, Hero_Attributes);

        //HeroEvents.OnXpGainHendler?.Invoke(Hero_Attributes.CurrentXP, Hero_Attributes.XPForLevelUP);

        //HeroEvents.OnHealthChangeHenlder?.Invoke(Hero_Attributes.CurrentHealth, Hero_Attributes.MaxHealth);
        //HeroEvents.OnManaChangeHendler?.Invoke(Hero_Attributes.CurrentMana, Hero_Attributes.MaxMana);

        //HeroEvents.OnHeroSpawnHendler?.Invoke(_heroSkills.GetAllSkillsData());
    }

    private void Update()
    {
        Ray = MainCamera.ScreenPointToRay(InputReader.Instance.MousePosition);

        HandleMovementAndTargeting();

        HandleHeroSkills();

        //Test delete later :
        if (Keyboard.current.dKey.wasPressedThisFrame)
        {
            ChangeHealth(true, 20f);
        }
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            ChangeHealth(false, 20f);
        }

        if (Keyboard.current.xKey.wasPressedThisFrame)
        {
            ChagneMana(false, 20f);
        }
        if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            ChagneMana(true, 20f);
        }

        if (Keyboard.current.vKey.wasPressedThisFrame)
        {
            GainXp(50f);
            Debug.Log($"{_heroSkills.GetSkillLevel(0)},{_heroSkills.GetSkillLevel(1)},{_heroSkills.GetSkillLevel(2)},{_heroSkills.GetSkillLevel(3)}");
        }

        if (Keyboard.current.zKey.wasPressedThisFrame)
        {
            _heroInventory.SetGold(true, 100);

            HeroEvents.OnGoldGainHendler?.Invoke(_heroInventory.GetGold());
        }
    }

    private void HandleMovementAndTargeting()
    {
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

                    Instantiate(Marker, _hit.point, Marker.transform.rotation);
                }
                else if (_hit.collider.gameObject.CompareTag("Enemy"))
                {
                    _agent.speed = Hero_Attributes.CurrentMoveSpeed;

                    CurrentTarget = _hit.collider.gameObject;
                }
                else if (_hit.collider.gameObject.CompareTag("Item"))
                {
                    CurrentTarget = null;

                    _agent.speed = Hero_Attributes.CurrentMoveSpeed;

                    _heroMovement.Move(_hit.point);
                }
            }
        }

        if (CurrentTarget != null)
        {
            bool isInRange = _heroMovement.IsInRange(CurrentTarget.transform.position, Hero_Attributes.CurrentAttackRange);

            _heroAttack.GetRangeToAttack(isInRange, CurrentTarget);
        }
    }

    private void HandleHeroSkills()
    {
        if (InputReader.Instance.QButton)
        {
            if (_heroSkills.GetSkillLevel(0) == 0)
            {
                Debug.Log("Skill not studied");
                return;
            }

            _heroSkills.ExecuteSkillById(0);
        }
        if (InputReader.Instance.WButton)
        {
            if (_heroSkills.GetSkillLevel(1) == 0)
            {
                Debug.Log("Skill not studied");
                return;
            }

            _heroSkills.ExecuteSkillById(1);
        }
        if (InputReader.Instance.EButton)
        {
            if (_heroSkills.GetSkillLevel(2) == 0)
            {
                Debug.Log("Skill not studied");
                return;
            }

            _heroSkills.ExecuteSkillById(2);
        }
        if (InputReader.Instance.RButton)
        {
            if (_heroSkills.GetSkillLevel(3) == 0)
            {
                Debug.Log("Skill not studied");
                return;
            }

            _heroSkills.ExecuteSkillById(3);
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

        HeroEvents.OnHealthChangeHenlder?.Invoke(Hero_Attributes.CurrentHealth, Hero_Attributes.MaxHealth);
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

        HeroEvents.OnManaChangeHendler?.Invoke(Hero_Attributes.CurrentMana, Hero_Attributes.MaxMana);
    }

    public void GainXp(float XpAmount)
    {
        Hero_Attributes.CurrentXP += XpAmount;

        if (Hero_Attributes.CurrentXP >= Hero_Attributes.XPForLevelUP)
        {
            Hero_Attributes.XPForLevelUP *= 1.5f;
            Hero_Attributes.XPForLevelUP = Mathf.Round(Hero_Attributes.XPForLevelUP * 10f) / 10f;

            Hero_Attributes.CurrentXP = 0f;

            LevelUp();
        }

        HeroEvents.OnXpGainHendler?.Invoke(Hero_Attributes.CurrentXP, Hero_Attributes.XPForLevelUP);
    }

    public void LevelUp()
    {
        Hero_Attributes.Lv += 1;

        // Change Later : 
        Hero_Attributes.MaxHealth += 50f;
        Hero_Attributes.MaxMana += 10f;
        Hero_Attributes.CurrentDamage += 20f;
        Hero_Attributes.CurrentMagicDamage += 20f;

        Hero_Attributes.PointsForLevelUpSckills += 1;

        int[] heroLevelOfSkills = {
            _heroSkills.GetSkillLevel(0),
            _heroSkills.GetSkillLevel(1),
            _heroSkills.GetSkillLevel(2),
            _heroSkills.GetSkillLevel(3)
        };

        HeroEvents.OnLevelUpHendler?.Invoke(heroLevelOfSkills, Hero_Attributes);

        HeroEvents.OnHealthChangeHenlder?.Invoke(Hero_Attributes.CurrentHealth, Hero_Attributes.MaxHealth);
        HeroEvents.OnManaChangeHendler?.Invoke(Hero_Attributes.CurrentMana, Hero_Attributes.MaxMana);
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            ItemController item = other.GetComponent<ItemController>();

            int ItemsCountInInventory = _heroInventory.GetItemsCount();

            if (item != null && ItemsCountInInventory < _heroInventory.GetMaxItemsInInventory())
            {
                _heroInventory.AddItemInInventory(item.GetItemData());

                item.DeleteItem();

                HeroEvents.OnItemTakeHendler?.Invoke(_heroInventory.GetItems());
            }
            else
            {
                Debug.Log("Can't take item because Inventory is full");
            }
        }
    }
}
