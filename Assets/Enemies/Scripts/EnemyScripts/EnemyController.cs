using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public static List<EnemyController> AllEnemies = new List<EnemyController>();

    private EnemyMovement _enemyMovement;
    private EnemyAnimationsController _enemyAnimationsContoller;

    [SerializeField] private EnemyConfig _baseEnemyAttributes;
    [SerializeField] private EnemyAttributes _currentEnemyAttributes;

    public bool isDead = false;

    private void Awake()
    {
        _currentEnemyAttributes = new EnemyAttributes(_baseEnemyAttributes);

        _enemyMovement = GetComponent<EnemyMovement>();
        _enemyAnimationsContoller = GetComponent<EnemyAnimationsController>();

        _enemyMovement.Init(this);
        _enemyAnimationsContoller.Init(this);
    }

    private void Update()
    {
        if (!isDead)
        {
            HandleEnemyMovement();
        }

        EnemyDied();
    }

    public void ChangeHealth(bool isGettingDamage, int damage)
    {
        if (isGettingDamage)
        {
            _currentEnemyAttributes.CurrentHealth -= damage;
        }
        else
        {
            _currentEnemyAttributes.CurrentHealth += damage;
        }

        if (_currentEnemyAttributes.CurrentHealth < 0)
        {
            _currentEnemyAttributes.CurrentHealth = 0;
        }
        else if (_currentEnemyAttributes.CurrentHealth > _currentEnemyAttributes.CurrentMaxHealth)
        {
            _currentEnemyAttributes.CurrentHealth = _currentEnemyAttributes.CurrentMaxHealth;
        }
    }

    private void HandleEnemyMovement()
    {
        foreach (var hero in HeroController.AllHeroes)
        {
            float distance = Vector3.Distance(transform.position, hero.transform.position);

            if (distance <= _currentEnemyAttributes.CurrentDetectionDistance)
            {
                _enemyMovement.PursuingTarget(hero.transform.position);
                return;
            }
        }
    }

    private void EnemyDied()
    {
        if (_currentEnemyAttributes.CurrentHealth <= 0)
        {
            isDead = true;

            StartCoroutine(PlayDead());
            
        }
    }
    private IEnumerator PlayDead()
    {
        _enemyAnimationsContoller.SetTrigger("Dead");

        yield return new WaitForSeconds(3f);
        
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
