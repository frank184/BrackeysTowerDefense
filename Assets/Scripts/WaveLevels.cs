using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(WaveSpawner))]
public class WaveLevels : MonoBehaviour {
    [SerializeField]
    private Text waveText;

    private WaveSpawner waveSpawner;

    private float counter;
    public int waveLevel = 1;

    private void Start()
    {
        waveSpawner = GetComponent<WaveSpawner>();
        waveSpawner.OnWaveSpawn += IncrementLevel;
        counter = waveSpawner.waveTimer;
        InvokeRepeating("WaveCount", waveSpawner.waveDelay, 1);
    }

    private void WaveCount()
    {
        waveText.text = Mathf.Ceil(counter).ToString();
        counter -= 1;
    }

    void IncrementLevel()
    {
        Debug.Log("WAVE " + waveLevel + " INCOMING!");
        waveSpawner.enemyPerWave = ++waveLevel;
        counter = waveSpawner.waveTimer;
    }
}
