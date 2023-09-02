using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aoeLogic : MonoBehaviour
{
    public int WandAnnoyance;
    public float timer = 0;
    public float coolDown = 5;
    public float stunStrength = 5;
    public float knockBack = 1;

    void FixedUpdate(){
        Collider2D coll = GetComponent<Collider2D>();
        ContactFilter2D filter = new ContactFilter2D().NoFilter();
        List<Collider2D> results = new List<Collider2D>();
        Physics2D.OverlapCollider(coll,filter,results);

        timer++;
        if (timer > coolDown){
            foreach(Collider2D c in results){
                if(c.tag == "Enemy"){
                Enemy enemy = c.gameObject.GetComponent<Enemy>();
                enemy.Annoyance += WandAnnoyance;
                enemy.Stun = stunStrength;
                enemy.KnockBack = knockBack;
            }
            timer = 0;
        }
    }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
         
    }
}
