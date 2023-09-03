using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LovePickup : MonoBehaviour
{
    private bool _collected = false;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if(c.tag == "Player" && !_collected)
        {
            _collected = true;
            GameData.Instance.currency += 1;
            animator.Play("LovePickup", 0);
        }
    }

    void FinishAnimation()
    {
        Destroy(gameObject);
    }
}
