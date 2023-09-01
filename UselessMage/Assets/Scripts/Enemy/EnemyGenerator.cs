using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public LevelGenerator levelGenerator;
    public GameObject[] enemyPrefabs;
    public int maxEnemiesPerRoom = 3;

    public void GenerateEnemy(GameObject enemyPrefab, GameObject roomAsset)
    {
        var enemy = Instantiate(enemyPrefab, roomAsset.transform);
    }

    public void GenerateEnemiesForRoom(string roomId, GameObject roomAsset)
    {

        for (int i = 0; i < maxEnemiesPerRoom; i++)
        {
            var randomEnemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)]; 
            GenerateEnemy(randomEnemy, roomAsset);
        }

    }

    public void GenerateEnemies()
    {
        if (levelGenerator.Rooms != null)
            foreach (var kvp in levelGenerator.Rooms)
                GenerateEnemiesForRoom(kvp.Key, kvp.Value);
        
    }

}
