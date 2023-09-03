using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChiBlast : MonoBehaviour
{
    public float speed = 3.0f;
    public Vector3 target;
    private Rigidbody2D rb;
    public Animator animator;

    public ElementType elementType;
    public Color[] colors;
    public ParticleSystem[] particles;
    public SpriteRenderer sprite;
    public float attackDamage;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite.color = colors[(int)elementType];
        // color particles
        var gradient = new Gradient();
        gradient.SetKeys(new GradientColorKey[] { new GradientColorKey(sprite.color, 0) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f) });
        foreach (var particle in particles)
        {
            var col = particle.colorOverLifetime;
            col.enabled = true;
            col.color = gradient; 
        }
    }

    void Update()
    {
        var distance = target - transform.position;
        var direction = Vector3.Normalize(distance);
        if (distance.magnitude <= 0.01f) {
            Explode();
        } else {
            rb.velocity = direction * speed;
        }
    }

    private void Explode()
    {
        rb.velocity = Vector3.zero;
        animator.Play("ChiBallExplode", 0);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.tag == "Player")
        {
            PlayerLogic player = coll.gameObject.GetComponent<PlayerLogic>();
            player.health -= (int)attackDamage; 
        }
    }

    public void Dispose()
    {
        Destroy(gameObject);
    }
}
