using System;
using UnityEngine;

public class SpawnManager : MonoSingleton<SpawnManager> {
    #region Fields
    public Action<int> OnEnemyCountChange;

    [SerializeField] private int enemyCount;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject powerupPrefab;
    [SerializeField] private float spawnRange = 9.0f;
    [SerializeField] private int waveNumber = 1;
    #endregion Fields

    #region Properties
    public int EnemyCount {
        get { return enemyCount; }
        private set { enemyCount = value; }
    }
    #endregion Properties

    #region MonoBehaviour
    private void OnDisable() { UnSubscribe(); }

    private void OnEnable() { Subscribe(); }

    private void Start() {
        GetInitialEnemyCount();
        SpawnPowerupPrefabs();
        SpawnEnemyWave(waveNumber);
    }
    #endregion MonoBehaviour

    #region Methods
    public void BroadcastEnemyCountChange() {
        OnEnemyCountChange?.Invoke(EnemyCount);
    }

    public void UpdateEnemyCount(int delta) {
        EnemyCount += delta;
        BroadcastEnemyCountChange();
    }

    private Vector3 GenerateSpawnPosition() {
        float spawnPosX = UnityEngine.Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = UnityEngine.Random.Range(-spawnRange, spawnRange);
        return new Vector3(spawnPosX, 0, spawnPosZ);
    }

    private void GetInitialEnemyCount() {
        enemyCount = FindObjectsOfType<Enemy>().Length;
    }

    private void MonitorEnemyCount(int newCount) {
        if(newCount == 0) {
            waveNumber++;
            SpawnEnemyWave(waveNumber);
            SpawnPowerupPrefabs();
        }
    }

    private void SpawnEnemyWave(int enemiesToSpawn) {
        for(int i = 0; i < enemiesToSpawn; i++) {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }

    private void SpawnPowerupPrefabs() {
        Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
    }

    private void Subscribe() {
        UnSubscribe();
        OnEnemyCountChange += MonitorEnemyCount;
    }

    private void UnSubscribe() {
        OnEnemyCountChange -= MonitorEnemyCount;
    }
    #endregion Methods
}