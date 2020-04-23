using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour{

    [SerializeField]
    private GameObject powerupPrefab;
    [SerializeField]
    private int enemyCount;
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private float spawnRange = 9.0f;
    [SerializeField]
    private int waveNumber = 1;

    // Start is called before the first frame update
    void Start(){

        SpawnPowerupPrefabs();
        SpawnEnemyWave(waveNumber);
       
    }

    // Update is called once per frame
    void Update(){

        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0) {
            waveNumber++; 
            SpawnEnemyWave(waveNumber);
            SpawnPowerupPrefabs();
        }
        
    }

    Vector3 GenerateSpawnPosition() {

        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomPos;

    }

    void SpawnEnemyWave(int enemiesToSpawn) {

        for (int i = 0; i < enemiesToSpawn; i++) {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);

        }
    }

    void SpawnPowerupPrefabs() {

        Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);

    }

}
