using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aoeLogic : MonoBehaviour
{
    public int WandAnnoyance;

    void FixedUpdate(){
        Collider2D coll = GetComponent<Collider2D>();
        ContactFilter2D filter = new ContactFilter2D().NoFilter();
        List<Collider2D> results = new List<Collider2D>();
        Physics2D.OverlapCollider(coll,filter,results);

        foreach(Collider2D c in results){
            if(c.tag == "Enemy"){
            Enemy enemy = c.gameObject.GetComponent<Enemy>();
            enemy.Annoyance += WandAnnoyance;
        }
    }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
         
    }
}
