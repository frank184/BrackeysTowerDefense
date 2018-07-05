using UnityEngine;
using System;

public class WaveSpawner : MonoBehaviour {
    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private Transform spawnPoint;

    public float waveDelay = 1f;
    public float waveTimer = 1f;
    public int enemyPerWave = 1;
    public float spawnInterval = 1f;

    private void Awake()
    {
        InvokeRepeating("SpawnWave", waveDelay, waveTimer);
    }

    void SpawnWave()
    {
        for (int i = 0; i < enemyPerWave; i++)
            Invoke("SpawnEnemy", i * spawnInterval);
        if (OnWaveSpawn != null) OnWaveSpawn();
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        if (OnEnemySpawn != null) OnEnemySpawn();
    }

    public Action OnWaveSpawn;
    public Action OnEnemySpawn;
}
