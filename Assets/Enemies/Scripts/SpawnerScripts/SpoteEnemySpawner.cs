using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class SpoteEnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] EnemiesToSpawn;

    [SerializeField] private int _spawnDelay;

    [SerializeField] private GameObject[] SpawnerSpots;

    private List<GameObject> _spawnedEnemies = new List<GameObject>();

    private void Start()
    {
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        _spawnedEnemies.Clear();

        float NextSpawnSpot = 0;

        for (int i = 0; i < EnemiesToSpawn.Length; i++)
        {
            GameObject enemy = Instantiate(EnemiesToSpawn[i],
                new Vector3(transform.position.x + NextSpawnSpot, transform.position.y, transform.position.z),
                EnemiesToSpawn[i].transform.rotation);

            enemy.GetComponent<EnemyController>().DestinationPosition = SpawnerSpots[i];

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
