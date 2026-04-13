using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public static List<EnemyController> AllEnemies = new List<EnemyController>();

    public int Health;
    public int MaxHealth;

    private void Update()
    {
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void ChangeHealth(bool isGettingDamage, int damage)
    {
        if (isGettingDamage)
        {
            Health -= damage;
        }
        else
        {
            Health += damage;
        }

        if (Health < 0)
        {
            Health = 0;
        }
        else if (Health > MaxHealth)
        {
            Health = MaxHealth;
        }
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
