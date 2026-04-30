using System.Collections;
using TMPro;
using UnityEngine;

public class UIEnemiesWaves : MonoBehaviour
{
    [SerializeField] private TMP_Text WaveCount;
    [SerializeField] private TMP_Text TimeToNextWave;

    private IEnumerator CountTimeToNextWave(int TimeForNextWave)
    {
        for(int i = TimeForNextWave; i >= 0; i--)
        {
            TimeToNextWave.text = $"Next Wave : {i:00}";

            yield return new WaitForSeconds(1f);
        }
    }

    private void SetWaveInfo(EnemyEventArgs EnemyArgs)
    {
        WaveCount.text = $"Wave : {EnemyArgs.NumberOfWave}";

        StartCoroutine(CountTimeToNextWave(EnemyArgs.TimeForNextWave));
    }

    private void OnEnable()
    {
        EnemyEvents.OnNextWaveSpawnHandler += SetWaveInfo;
    }

    private void OnDisable()
    {
        EnemyEvents.OnNextWaveSpawnHandler -= SetWaveInfo;
    }
}
