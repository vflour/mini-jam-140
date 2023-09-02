using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aoeLogic : MonoBehaviour
{
    public int WandAnnoyance;
    public float timer = 0;
    public float coolDown = 1;
    public float stunStrength = 5;
    public float knockBack = 1;
    public Collider2D coll;
    public Animator animator;

    void FixedUpdate(){
        coll = GetComponent<Collider2D>();
        ContactFilter2D filter = new ContactFilter2D().NoFilter();
        List<Collider2D> results = new List<Collider2D>();
        Physics2D.OverlapCollider(coll,filter,results);

        timer++;
        if (timer > coolDown){
            foreach(Collider2D c in results){
                //if(c.tag == "Enemy"){
                //Enemy enemy = c.gameObject.GetComponent<Enemy>();
                //enemy.Annoyance += WandAnnoyance;
                //enemy.Stun = stunStrength;
                //enemy.KnockBack = knockBack;
            //}
            timer = 0;
        }
    }
    }
    
    public void ToggleEnabled(bool enabled)
    {
        if (enabled)
            animator.SetTrigger("Summon");
        coll.enabled = enabled;
    }

    public void FinishSummonAnimation()
    {
        ToggleEnabled(false);
    }

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        Debug.Log(c);
        if(c.tag == "Enemy"){
            Enemy enemy = c.gameObject.GetComponent<Enemy>();
            enemy.Annoyance += WandAnnoyance;
            enemy.Stun = stunStrength;
            enemy.KnockBack = knockBack;    
        }
    }
}
