using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(WaveSpawner))]
public class WaveLevels : MonoBehaviour {
    [SerializeField]
    private Text timeText;
    [SerializeField]
    private Text waveText;

    private WaveSpawner waveSpawner;

    public EnemyBlueprint enemyBlueprint;

    private float counter;
    public int waveLevel = 0;

    private bool startCounting = false;

    private void Start()
    {
        waveSpawner = GetComponent<WaveSpawner>();
        waveSpawner.OnWaveSpawn += SetupNextWave;
        counter = waveSpawner.waveTimer;
        Invoke("StartCounting", waveSpawner.waveDelay);
        waveText.text = "Wave " + waveLevel;
    }

    private void Update()
    {
        if (startCounting)
        {
            timeText.text = string.Format("{00:00.00}", counter);
            counter -= Time.deltaTime;
        }
    }

    void SetupNextWave()
    {
        waveLevel += 1;
        waveText.text = "Wave " + waveLevel;
        counter = waveSpawner.waveTimer;
        waveSpawner.SetEnemyBlueprint(enemyBlueprint);
        waveSpawner.enemyPerWave += 1;
        counter = waveSpawner.waveTimer;
        enemyBlueprint.speed += 0.1f;
        enemyBlueprint.health += 0.2f;
    }

    private void StartCounting()
    {
        startCounting = true;
    }
}
