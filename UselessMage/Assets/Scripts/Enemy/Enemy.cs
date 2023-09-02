using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int Annoyance = 0;
    public float Stun = 0;
    public float KnockBack = 0;

    public int maxAnnoyance = 100;
    public GameObject target;
    public float moveSpeed;
    private Rigidbody2D rb;

    public int attackDamage;
    public float damageRange;

    void Start(){
        rb = this.GetComponent<Rigidbody2D>();
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
}
