using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    public float Stun = 0;
    public float KnockBack = 0;

    public GameObject target;
    public float moveSpeed;
    private Rigidbody2D rb;

    public int attackDamage;
    public float damageRange;

    void Start(){
        rb = this.GetComponent<Rigidbody2D>();
        enemyAnimator = GetComponent<Animator>();
    }

    void FixedUpdate(){
        Vector3 targetDir = target.transform.position - transform.position;
        rb.velocity = targetDir * moveSpeed;
        var opposite = -rb.velocity;
        
        if(Stun>0){
            rb.velocity = Vector2.zero;
            Stun--;
        }

        if(KnockBack>0){
            rb.velocity = opposite * KnockBack;
            KnockBack = 0;
        }

        var nearTarget = Vector2.Distance(this.transform.position,target.transform.position);
        if (nearTarget < damageRange){
            target.GetComponent<PlayerLogic>().health -= attackDamage;
        }

        if(Annoyance>=maxAnnoyance){
            Destroy(this.gameObject);
        }
    }
    
    [field: SerializeField]
    private AnnoyanceState _annoyanceState;
    public AnnoyanceState EnemyAnnoyanceState 
    {
        private set
        {
            _annoyanceState = value;
            enemyAnimator.Play(_annoyanceState.ToString(), -1, 0);
        }
        get
        {
            return _annoyanceState;
        }

    }
    
    private int _annoyance;
    public int Annoyance 
    {
        set 
        {
            bool hit = _annoyance > value;
            _annoyance = Mathf.Clamp(value, 0, maxAnnoyance);
            
            if (!hit) return;
            
            bool firstHit = EnemyAnnoyanceState == AnnoyanceState.Idle; 
            if (firstHit)
                EnemyAnnoyanceState = AnnoyanceState.Surprised;
            else if (_annoyance == 0)
                EnemyAnnoyanceState = AnnoyanceState.Enraged;
            else if (0.5f <= _annoyance/maxAnnoyance )
                EnemyAnnoyanceState = AnnoyanceState.Annoyed;
                
        }
        get 
        {
            return _annoyance;
        }
    }

    public int maxAnnoyance = 100; 
    
    [Header("Enemy Assets")]
    public Animator enemyAnimator;

}

public enum AnnoyanceState
{
    Idle,
    Surprised,
    Annoyed,
    Enraged 
}
