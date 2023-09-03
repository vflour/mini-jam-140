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
    public GameObject loveDrop;
    public ElementType elementType;
    public RoomEnemyData roomData;

    public int attackDamage;
    public float damageRange;

    private void DropLove()
    {
        var drop = Instantiate(loveDrop, transform.parent);
        drop.transform.position = transform.position;
    }

    void Start(){
        rb = this.GetComponent<Rigidbody2D>();
        enemyAnimator = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
    }

    void FixedUpdate(){
        var camera = Camera.main;
        Vector3 targetDir = EnemyAnnoyanceState != AnnoyanceState.Enraged ? target.transform.position - transform.position : OffscreenTarget - transform.position;
        rb.velocity = targetDir * moveSpeed;
        var opposite = -rb.velocity;
        var screenPoint = camera.WorldToScreenPoint(transform.position);
       
        if (EnemyAnnoyanceState != AnnoyanceState.Enraged)
        {
            if(Stun>0){
                rb.velocity = Vector2.zero;
                Stun--;
            }
            else {
                enemyAnimator.SetBool("Stunned", false);
            }

            if(KnockBack>0){
                //rb.AddRelativeForce(Vector3.Normalize(opposite) * KnockBack * 0.1f, ForceMode2D.Impulse);
                KnockBack = KnockBack/2;
            }
        
            var nearTarget = Vector2.Distance(this.transform.position,target.transform.position);
            if (nearTarget < damageRange){
                target.GetComponent<PlayerLogic>().health -= attackDamage;
            }
        }
        else if(screenPoint.x <= -50f || screenPoint.x >= camera.pixelWidth + 50f) {
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
                var camera = Camera.main;
                var position = camera.WorldToScreenPoint(transform.position);
                var extremity = position.x >= camera.pixelWidth/2 ? camera.pixelWidth + 2000 : -2000;
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
            
            enemyAnimator.Play(value.ToString(), 0, 0);
            if (_annoyanceState == value) return;
            _annoyanceState = value;
            

            if (_annoyanceState == AnnoyanceState.Enraged)
            {
                collider.enabled = false; 
                roomData.enemyCount -= 1; 
                DropLove();
            }

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
            _annoyance = Mathf.Clamp(value, 0, maxAnnoyance);
            if (!hit) return;
             
            bool firstHit = EnemyAnnoyanceState == AnnoyanceState.Idle; 
            if (firstHit)
                EnemyAnnoyanceState = AnnoyanceState.Surprised;
            else if (_annoyance >= maxAnnoyance)
                EnemyAnnoyanceState = AnnoyanceState.Enraged;
            else if (0.5f <= _annoyance/(float)maxAnnoyance)
                EnemyAnnoyanceState = AnnoyanceState.Annoyed;
                
        }
        get 
        {
            return _annoyance;
        }
    }

    public void TriggerElementStun()
    {
        if (EnemyAnnoyanceState != AnnoyanceState.Enraged)
            enemyAnimator.Play("ElementStunEnter", 0, 0);
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

public enum ElementType
{
    Grass,
    Ice,
    Fire,
    Neutral
}
