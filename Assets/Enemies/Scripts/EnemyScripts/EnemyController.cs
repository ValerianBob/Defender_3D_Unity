using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public static List<EnemyController> AllEnemies = new List<EnemyController>();

    private EnemyMovement _enemyMovement;
    private EnemyAttack _enemyAttack;
    private EnemyAnimationsController _enemyAnimationsContoller;
    private EnemyHealthBarUI _healthBarUI;

    private CapsuleCollider _capsuleCollider;

    [SerializeField] private EnemyConfig _baseEnemyAttributes;
    [SerializeField] private EnemyAttributes _currentEnemyAttributes;

    public GameObject DestinationPosition;

    public GameObject CurrentTarget;

    public bool isDead = false;
    
    private void Awake()
    {
        _currentEnemyAttributes = new EnemyAttributes(_baseEnemyAttributes);

        _capsuleCollider = GetComponent<CapsuleCollider>();

        _enemyMovement = GetComponent<EnemyMovement>();
        _enemyAttack = GetComponent<EnemyAttack>();
        _enemyAnimationsContoller = GetComponent<EnemyAnimationsController>();
        _healthBarUI = GetComponent<EnemyHealthBarUI>();

        _enemyMovement.Init(this);
        _enemyAttack.Init(this);
        _enemyAnimationsContoller.Init(this);
        _healthBarUI.Init(this);

        if (_currentEnemyAttributes.EnemyType == EnemyType.BaseRaider)
        {
            DestinationPosition = GameObject.FindGameObjectWithTag("Base");
        }
    }

    private void Start()
    {
        StartCoroutine(GainAttributesLoop());
    }

    private void Update()
    {
        if (!isDead)
        {
            FindTarget();
            HandleEnemyMovement();
        }

        EnemyDied();
    }

    public void ChangeHealth(bool isGettingDamage, int damage)
    {
        if (isGettingDamage)
        {
            _currentEnemyAttributes.CurrentHealth -= damage;

            if (_currentEnemyAttributes.CurrentHealth < 0)
            {
                _currentEnemyAttributes.CurrentHealth = 0;
            }

            _healthBarUI.SetHealthBarInfo(_currentEnemyAttributes.CurrentHealth, _currentEnemyAttributes.CurrentMaxHealth);
        }
        else
        {
            _currentEnemyAttributes.CurrentHealth += damage;

            if (_currentEnemyAttributes.CurrentHealth > _currentEnemyAttributes.CurrentMaxHealth)
            {
                _currentEnemyAttributes.CurrentHealth = _currentEnemyAttributes.CurrentMaxHealth;
            }

            _healthBarUI.SetHealthBarInfo(_currentEnemyAttributes.CurrentHealth, _currentEnemyAttributes.CurrentMaxHealth);
        }
    }

    private IEnumerator GainAttributesLoop()
    {
        while (true)
        {
            ChangeHealth(false, _currentEnemyAttributes.CurrentHealthGain);

            yield return new WaitForSeconds(1f);
        }
    }

    private void HandleEnemyMovement()
    {
        if (CurrentTarget != null)
        {
            _enemyMovement.PursuingTarget(CurrentTarget.transform.position);
        }
        else
        {
            _enemyMovement.Move(DestinationPosition.transform.position);
        }
    }

    private void FindTarget()
    {
        GameObject closestTarget = null;

        foreach (var hero in HeroController.AllHeroes)
        {
            if (hero == null)
            {
                continue;
            }

            if (hero.Hero_Attributes.CurrentHealth <= 0)
            {
                continue;
            }
            
            float distance = Vector3.Distance(transform.position, hero.transform.position);

            if (distance <= _currentEnemyAttributes.CurrentDetectionDistance)
            {
                closestTarget = hero.gameObject;
            }
        }

        if (closestTarget != null)
        {
            CurrentTarget = closestTarget;
        }
        else
        {
            CurrentTarget = (_currentEnemyAttributes.EnemyType == EnemyType.BaseRaider) ? DestinationPosition : null;
        }
    }

    public bool IsInAttackRange()
    {
        if (CurrentTarget != null)
        {
            float distance = Vector3.Distance(transform.position, CurrentTarget.transform.position);

            if (_currentEnemyAttributes.CurrentAttackRange >= distance)
            {
                return true;
            }
            return false;
        }
        else
        {
            return false;
        }
    }

    private void EnemyDied()
    {
        if (_currentEnemyAttributes.CurrentHealth <= 0 && !isDead)
        {
            isDead = true;

            _capsuleCollider.isTrigger = true;

            StartCoroutine(PlayDead());

            _enemyAnimationsContoller.SetAnimatorRootMotion();

            _healthBarUI.HideHealthBarSlider();

            EnemyEventArgs EnemyArgs = new EnemyEventArgs(this, gameObject);
            EnemyEvents.OnEnemyDeathHandler?.Invoke(EnemyArgs);

            Results.Instance.Kills += 1;
        }
    }
    private IEnumerator PlayDead()
    {
        _enemyAnimationsContoller.SetTrigger("Dead");

        yield return new WaitForSeconds(5f);
        
        Destroy(gameObject);
    }

    public EnemyAttributes GetEnemyCurrentAttributes()
    {
        return _currentEnemyAttributes;
    }

    public float GetNavMeshMagnitude()
    {
        return _enemyMovement._agent.velocity.magnitude;
    }

    private void OnEnable()
    {
        AllEnemies.Add(this);
    }

    private void OnDisable()
    {
        AllEnemies.Remove(this);
    }
}
