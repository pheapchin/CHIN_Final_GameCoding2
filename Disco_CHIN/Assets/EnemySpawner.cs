using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabsList;
    //predefined spawn locations
    public Transform[] spawnPointsList;

    public int waveNumber = 1;
    public float timeBetweenWaves = 10f;
    public int enemiesPerWave = 3;

    private int enemiesAlive = 0;

    // Start is called before the first frame update
    void Start()
    {
        //spawns immediately
        SpawnEnemies();
        StartCoroutine(SpawnWave());
    }

   IEnumerator SpawnWave()
    {
        while (true)//infiite loop to spaw enemies, change it to a reuirement to turn it to true so i can stop waves
        {
            yield return new WaitForSeconds(timeBetweenWaves);
            SpawnEnemies();
        }
    }

    private void SpawnEnemies()
    {
        for(int i = 0; i < enemiesPerWave;  i++)
        {
            Transform spawnPoint = spawnPointsList[Random.Range(0, spawnPointsList.Length)];

            GameObject enemyPrefab = enemyPrefabsList[Random.Range(0, enemyPrefabsList.Length)];
            //create an enemy at chosen spawn point
            GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            //increase count of enemies
            enemiesAlive ++;
        }
        //increase wave #
        waveNumber++;
        enemiesPerWave += 2;
    }
}
