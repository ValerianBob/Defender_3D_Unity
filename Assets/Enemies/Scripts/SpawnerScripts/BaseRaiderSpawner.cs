using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseRaiderSpawner : MonoBehaviour
{
    [Serializable]
    public struct EnemiesWaves
    {
        public GameObject[] EnemiesPrefabs;
    }

    [SerializeField] private EnemiesWaves[] Enemies;

    [SerializeField] private GameObject[] SpotsToSpawn;

    [SerializeField] private int TimeToSpawnEnemies;

    [SerializeField] private List<GameObject> CurrentSpawnedEnemies;

    public int WaveCount;

    public bool IsRespawnEnemies = false;

    private void Start()
    {
        WaveCount = 0;

        StartCoroutine(CountNextWave());
    }

    private void Update()
    {
        if (WaveCount == 10 && CheckAliveEnemies() && !GameStateController.Instance.GameOver && !IsRespawnEnemies)
        {
            GameStateController.Instance.GameOver = true;

            GameStateController.OnGameWin?.Invoke();
        }

        if (!GameStateController.Instance.GameOver)
        {
            if (!IsRespawnEnemies && CheckAliveEnemies())
            {
                StartCoroutine(CountNextWave());
            }
        }
    }

    private bool CheckAliveEnemies()
    {
        foreach (var enemy in CurrentSpawnedEnemies)
        {
            if (enemy != null)
            {
                return false;
            }
        }
        return true;
    }

    private IEnumerator SpawnEnemies()
    {
        CurrentSpawnedEnemies.Clear();

        for (int i = 0; i < Enemies[WaveCount - 1].EnemiesPrefabs.Length; i++)
        {
            int SpawnPointId = 0;

            if (i % 2 == 0)
            {
                SpawnPointId = 0;
            }
            else
            {
                SpawnPointId = 1;
            }

            GameObject tempEnemy = Instantiate(Enemies[WaveCount - 1].EnemiesPrefabs[i], 
                SpotsToSpawn[SpawnPointId].transform.position, 
                Enemies[WaveCount - 1].EnemiesPrefabs[i].transform.rotation);

            CurrentSpawnedEnemies.Add(tempEnemy);

            yield return new WaitForSeconds(1f);
        }

        IsRespawnEnemies = false;
    }

    private IEnumerator CountNextWave()
    {
        IsRespawnEnemies = true;

        WaveCount += 1;

        EnemyEventArgs EnemyArgs = new EnemyEventArgs(WaveCount, TimeToSpawnEnemies);
        EnemyEvents.OnNextWaveSpawnHandler?.Invoke(EnemyArgs);

        yield return new WaitForSeconds(TimeToSpawnEnemies);

        StartCoroutine(SpawnEnemies());
    }
}
