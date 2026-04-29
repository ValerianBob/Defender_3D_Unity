using System;
using System.Collections;
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

    public int WaveCount;

    public int CountTimeToNextWave;

    private void Start()
    {
        CountTimeToNextWave = TimeToSpawnEnemies;
        WaveCount = 0;

        StartCoroutine(CountNextWave());
    }

    private IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < Enemies[WaveCount].EnemiesPrefabs.Length; i++)
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

            Instantiate(Enemies[WaveCount].EnemiesPrefabs[i], 
                SpotsToSpawn[SpawnPointId].transform.position, 
                Enemies[WaveCount].EnemiesPrefabs[i].transform.rotation);

            yield return new WaitForSeconds(1f);
        }
    }

    private IEnumerator CountNextWave()
    {
        WaveCount += 1;

        yield return new WaitForSeconds(WaveCount);

        StartCoroutine(SpawnEnemies());
    }
}
