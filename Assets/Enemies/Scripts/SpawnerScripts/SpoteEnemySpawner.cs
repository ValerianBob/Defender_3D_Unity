using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpoteEnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] EnemiesToSpawn;

    [SerializeField] private int _spawnDelay;

    private List<GameObject> _spawnedEnemies = new List<GameObject>();

    private void Start()
    {
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        _spawnedEnemies.Clear();

        float NextSpawnSpot = 0;

        foreach (var item in EnemiesToSpawn)
        {
            GameObject enemy = Instantiate(
                item, 
                new Vector3(transform.position.x + NextSpawnSpot, transform.position.y, transform.position.z), 
                item.transform.rotation);

            _spawnedEnemies.Add(enemy);

            NextSpawnSpot += 1;
        }
    }

    private void HandleEnemyDeath(EnemyEventArgs args)
    {
        GameObject deadEnemy = args.CurrentEnemyObject;

        if (_spawnedEnemies.Contains(deadEnemy))
        {
            _spawnedEnemies.Remove(deadEnemy);

            if (_spawnedEnemies.Count == 0)
            {
                StartCoroutine(RespawnRoutine());
            }
        }
    }

    private IEnumerator RespawnRoutine()
    {
        yield return new WaitForSeconds(_spawnDelay);
        SpawnEnemies();
    }

    private void OnEnable()
    {
        EnemyEvents.OnEnemyDeathHandler += HandleEnemyDeath;
    }

    private void OnDisable()
    {
        EnemyEvents.OnEnemyDeathHandler -= HandleEnemyDeath;
    }
}
