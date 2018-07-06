using UnityEngine;
using System;

public class WaveSpawner : MonoBehaviour {
    [SerializeField]
    private Transform spawnPoint;

    public float waveDelay = 1f;
    public float waveTimer = 1f;
    public int enemyPerWave = 1;
    public float spawnInterval = 1f;

    private EnemyBlueprint enemyBlueprint;

    private void Awake()
    {
        InvokeRepeating("SpawnWave", waveDelay, waveTimer);
    }

    public void SetEnemyBlueprint(EnemyBlueprint enemyBlueprint)
    {
        this.enemyBlueprint = enemyBlueprint;
    }

    void SpawnWave()
    {
        for (int i = 0; i < enemyPerWave; i++)
            Invoke("SpawnEnemy", i * spawnInterval);
        if (OnWaveSpawn != null) OnWaveSpawn();
    }

    void SpawnEnemy()
    {
        GameObject enemy = Instantiate(enemyBlueprint.enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        enemy.GetComponent<Enemy>().health = enemyBlueprint.health;
        enemy.GetComponent<Enemy>().reward = enemyBlueprint.reward;
        enemy.GetComponent<EnemyMovement>().speed = enemyBlueprint.speed;
        if (OnEnemySpawn != null) OnEnemySpawn();
    }

    public Action OnWaveSpawn;
    public Action OnEnemySpawn;
}
