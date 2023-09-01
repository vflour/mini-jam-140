using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEnemyData : MonoBehaviour
{
    public int enemyCount = 0;
    public int maxEnemyCount = 5;
    public BoxCollider2D spawnBoundingBox;

    public Vector3 GetRandomPosition()
    {

        var bounds = spawnBoundingBox.bounds;
        var x = Random.Range(bounds.min.x, bounds.max.x);
        var y = Random.Range(bounds.min.y, bounds.max.y);
        var z = Random.Range(bounds.min.z, bounds.max.z);
        return new Vector3(x, y, z);
    
    }

}
