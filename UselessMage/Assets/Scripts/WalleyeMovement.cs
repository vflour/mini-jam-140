using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalleyeMovement : MonoBehaviour
{
    public float speed = 0.1f;
    public Transform[] points;
    private int _currentPoint;
    private Rigidbody2D rb;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (_currentPoint > points.Length)
            return;

        var point = points[_currentPoint];
        var distance = point.position - transform.position;
        if (distance.magnitude <= 0.1f)
            _currentPoint = (_currentPoint + 1 ) % points.Length; 

        var direction = Vector3.Normalize(distance);
        rb.velocity = direction * speed;
        animator.SetFloat("Horizontal", direction.x);
    }

}
