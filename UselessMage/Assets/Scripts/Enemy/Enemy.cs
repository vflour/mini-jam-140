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
    private Collider2D collider;

    public int attackDamage;
    public float damageRange;

    void Start(){
        rb = this.GetComponent<Rigidbody2D>();
        enemyAnimator = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
    }

    void FixedUpdate(){
        Vector3 targetDir = EnemyAnnoyanceState != AnnoyanceState.Enraged ? target.transform.position - transform.position : OffscreenTarget - transform.position;
        rb.velocity = targetDir * moveSpeed;
        var opposite = -rb.velocity;
       
        if (EnemyAnnoyanceState != AnnoyanceState.Enraged)
        {
            if(Stun>0){
                rb.velocity = Vector2.zero;
                Stun--;
            }

            if(KnockBack>0){
                rb.AddRelativeForce(Vector3.Normalize(opposite) * KnockBack * 0.1f, ForceMode2D.Impulse);
                KnockBack = KnockBack/2;
            }
        
            var nearTarget = Vector2.Distance(this.transform.position,target.transform.position);
            if (nearTarget < damageRange){
                target.GetComponent<PlayerLogic>().health -= attackDamage;
            }
        }
        else if(targetDir.magnitude < 0.01) {
            Destroy(this.gameObject);
        }
    }

    // cached value
    private Vector3 _offscreenTarget = Vector3.zero;
    private Vector3 OffscreenTarget { 
        get 
        {
            if (_offscreenTarget == Vector3.zero)
            {
                var camera = Camera.current;
                var position = camera.WorldToScreenPoint(transform.position);
                var extremity = position.x >= camera.pixelWidth/2 ? camera.pixelWidth + 100 : -100;
                _offscreenTarget = camera.ScreenToWorldPoint(new Vector3(extremity, position.y));
            }
            return _offscreenTarget;
        }
    }
    
    [field: SerializeField]
    private AnnoyanceState _annoyanceState;
    public AnnoyanceState EnemyAnnoyanceState 
    {
        private set
        {
            _annoyanceState = value;
            enemyAnimator.Play(_annoyanceState.ToString(), 0, 0);
            if (_annoyanceState == AnnoyanceState.Enraged)
                collider.enabled = false;
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
            bool hit = _annoyance <= value;
            Debug.Log(_annoyance);
            _annoyance = Mathf.Clamp(value, 0, maxAnnoyance);
            
            if (!hit) return;
             
            bool firstHit = EnemyAnnoyanceState == AnnoyanceState.Idle; 
            if (firstHit)
                EnemyAnnoyanceState = AnnoyanceState.Surprised;
            else if (_annoyance >= maxAnnoyance)
                EnemyAnnoyanceState = AnnoyanceState.Enraged;
            else if (0.5f <= _annoyance/(float)maxAnnoyance )
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
