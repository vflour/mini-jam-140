using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerLogic : MonoBehaviour
{
    public float movementSpeed;
    private Rigidbody2D rb;
    private Collider2D coll;
    private Animator animator;
    private Vector2 movementDirection;
    public GameObject aoeRadiusGFX;
    public int maxhealth;
    
    public CameraFollow follow;
    private int _health;

    public bool IsDefeated => _health <= 0;
    
    public int health {
        get 
        {
            return _health;
        }

        set 
        {
            var clampedValue = Mathf.Clamp(value, 0, maxhealth);
            if (_health == clampedValue) return;
            _health = clampedValue;             
            
            if (IsDefeated)
            {
                follow.target = null;
                coll.enabled = false;
                _deathLungeDirection = Vector3.up*2.5f + new Vector3(Random.Range(-2.0f, 2.0f), 0);
                _deathLungeTime = deathTime;
                animator.Play("Death", -1 ,0);
            }
        }
    }
    public TMP_Text healthtext;

    public float deathTime = 1f;
    public float peakTime = 0.15f;
    private float _deathLungeTime = 0;
    private Vector3 _deathLungeDirection = Vector3.up;

    void Start()
    {
        health = maxhealth;
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        follow = Camera.main.GetComponent<CameraFollow>();
        animator = GetComponent<Animator>();
        
    }

    void Update()
    {
        movementDirection = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
    }

    void FixedUpdate() {
        if (!IsDefeated)
            rb.velocity = movementDirection * movementSpeed;
        else
        {
            if (_deathLungeTime >= deathTime - peakTime)
            {
                rb.velocity = _deathLungeDirection;
            }
            else if(_deathLungeTime > 0)
                rb.gravityScale = 1f;
            else
                SceneManager.LoadScene("hub"); 
            
            _deathLungeTime -= Time.deltaTime;
        }

        float percentage = (float)health/(float)maxhealth*100;
        healthtext.SetText(percentage + "%");
    }
}
