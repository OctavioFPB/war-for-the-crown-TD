using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

[System.Serializable]

public class WaveData{

    public float duration = 10f;
    public int normalEnemies = 5;
    public int fastEnemies = 2;
    public int strongEnemies = 1;

}

public class WaveManager : MonoBehaviour
{
    public WaveData[] waves;
    public Button startWaveButton;

    public GameObject normalEnemyPrefab;
    public GameObject fastEnemyPrefab;
    public GameObject strongEnemyPrefab;

    public Transform[] wayPoints;

    public TextMeshProUGUI waveTxt;

    private int currentWaveIndex = 0;
    private bool waveRunning = false;

    void Start()
    {
        startWaveButton.onClick.AddListener(StartWave);
    }

    public void StartWave()
    {
        if (waveRunning) return;
        if (currentWaveIndex >= waves.Length) return;

        StartCoroutine(RunWave());

    }

    IEnumerator RunWave() 
    {
        waveRunning = true;
        startWaveButton.interactable = false;

        WaveData wave = waves[currentWaveIndex];

        for(int i = 0; i < wave.normalEnemies; i++)
        {
            SpawnEnemy(normalEnemyPrefab);
            yield return new WaitForSeconds((wave.duration / 3) / wave.normalEnemies);
        }

        for(int i = 0; i < wave.fastEnemies; i++)
        {
            SpawnEnemy(fastEnemyPrefab);
            yield return new WaitForSeconds((wave.duration / 3) / wave.fastEnemies);
        }

        for(int i = 0; i < wave.strongEnemies; i++)
        {
            SpawnEnemy(strongEnemyPrefab);
            yield return new WaitForSeconds((wave.duration / 3) / wave.strongEnemies);
        }

        yield return new WaitForSeconds(wave.duration / 3);
        waveRunning = false;
        startWaveButton.interactable = true;
        currentWaveIndex++;
        waveTxt.text = (currentWaveIndex + 1).ToString();
        
    }

    void SpawnEnemy(GameObject prefab)
    {
        GameObject e = Instantiate(prefab, wayPoints[0].position, Quaternion.identity);
        Enemy enemy = e.GetComponent<Enemy>();
        enemy.waypoints = wayPoints;
    }

}
