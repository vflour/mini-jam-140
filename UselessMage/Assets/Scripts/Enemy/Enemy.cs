using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int Annoyance = 0;
    public int maxAnnoyance = 100;
    public GameObject target;
    public float moveSpeed;
    private Rigidbody2D rb;

    void Start(){
        rb = this.GetComponent<Rigidbody2D>();
    }
    void FixedUpdate(){
        Vector3 targetDir = target.transform.position - transform.position;
        rb.velocity = targetDir * moveSpeed;

        if(Annoyance>=maxAnnoyance){
            Destroy(this.gameObject);
        }
    }
}
