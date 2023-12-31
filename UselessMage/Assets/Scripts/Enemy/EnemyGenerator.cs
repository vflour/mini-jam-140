using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public LevelGenerator levelGenerator;
    public GameObject[] enemyPrefabs;
    public GameObject target;
    public GameObject loveDrop;

    public void GetEnemyAssets()
    {
        enemyPrefabs = Resources.LoadAll<GameObject>("Monsters/" + GameData.Instance.CurrentLevel);
    }

    public void GenerateEnemy(GameObject enemyPrefab, RoomEnemyData roomData)
    {
        var position = roomData.GetRandomPosition();
        var enemy = Instantiate(enemyPrefab, roomData.transform);
        enemy.transform.localPosition = position;
        var enemyData = enemy.GetComponent<Enemy>();
        enemyData.target = target;
        enemyData.loveDrop = loveDrop;
        enemyData.roomData = roomData;
        
        roomData.enemyCount++;
    }

    public void GenerateEnemiesForRoom(string roomId, GameObject roomAsset)
    {
        RoomEnemyData roomData = roomAsset.GetComponent<RoomEnemyData>(); 
        if (roomData == null) return;
        var enemiesPerRoom = roomData.maxEnemyCount - roomData.enemyCount;

        for (int i = 0; i < enemiesPerRoom; i++)
        {
            var randomEnemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)]; 
            GenerateEnemy(randomEnemy, roomData);
        }

    }
    
    public float spawnRate = 10f;
    private float _spawnCooldown = 0;

    void Update()
    {
        _spawnCooldown -= Time.deltaTime;
        if (_spawnCooldown <= 0)
        {
            _spawnCooldown = spawnRate;
            GenerateEnemies();
        }
    }

    void Awake()
    {
        _spawnCooldown = spawnRate;
        GetEnemyAssets();
    }

    public void GenerateEnemies()
    {
        if (levelGenerator.Rooms != null)
            foreach (var kvp in levelGenerator.Rooms)
                GenerateEnemiesForRoom(kvp.Key, kvp.Value);
        
    }

}
