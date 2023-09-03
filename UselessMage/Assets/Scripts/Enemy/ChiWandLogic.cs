using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChiWandLogic : MonoBehaviour
{
    public GameObject blastSFX; 
    public float blastFireRate = 5f;
    private float _blastCooldown;
    private ChiEnemy _enemy;

    void Start()
    {
        _blastCooldown = blastFireRate;
        _enemy = GetComponent<ChiEnemy>();
    }

    void Update()
    {
        _blastCooldown -= Time.deltaTime;
        if (_blastCooldown <= 0)
        {
            _blastCooldown = blastFireRate;
            ChiBlast blast = Instantiate(blastSFX, transform.position, Quaternion.identity, transform).GetComponent<ChiBlast>();
            blast.target = _enemy.target.transform.position;
            blast.elementType = _enemy.elementType;
            blast.attackDamage = _enemy.attackDamage*10f;
        }
    }
}
