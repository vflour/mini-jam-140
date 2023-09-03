using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aoeLogic : MonoBehaviour
{
    public static Dictionary<ElementType, ElementType> effectiveness = new Dictionary<ElementType, ElementType> 
    {
        { ElementType.Fire, ElementType.Grass },
        { ElementType.Ice, ElementType.Fire },
        { ElementType.Grass, ElementType.Ice }
    };

    public int WandAnnoyance;
    public float timer = 0;
    public float coolDown = 1;
    public float stunStrength = 5;
    public float knockBack = 1;
    public Collider2D coll;
    public Animator animator;
    public ElementType elementType;

    void FixedUpdate(){
        coll = GetComponent<Collider2D>();
        ContactFilter2D filter = new ContactFilter2D().NoFilter();
        List<Collider2D> results = new List<Collider2D>();
        Physics2D.OverlapCollider(coll,filter,results);
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
        if(c.tag == "Enemy"){
            Enemy enemy = c.gameObject.GetComponent<Enemy>();
            AnnoyEnemy(enemy);   
        }
    }

    private float GetElementMultiplier(Enemy enemy)
    {
        if (elementType == ElementType.Neutral) return 1.0f;
        // if the enemy is weak to the wand
        if (enemy.elementType == effectiveness[elementType])
            return 1.5f;
        // if the wand is weak against the enemy
        if (elementType == effectiveness[enemy.elementType])
            return 0.5f;
        return 1.0f;
    }

    private void AnnoyEnemy(Enemy enemy)
    {
        float elemMulti = GetElementMultiplier(enemy);
        enemy.Annoyance += (int)(WandAnnoyance * elemMulti);
        enemy.Stun = (int)(stunStrength * elemMulti);
        if (elemMulti > 1)
            enemy.TriggerElementStun();
        enemy.KnockBack = knockBack; 
    }
}
