using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerLogic : MonoBehaviour
{
    public float movementSpeed;
    private Rigidbody2D rb;
    private Vector2 movementDirection;
    public GameObject aoeRadiusGFX;
    public int maxhealth;
    public int health;
    public TMP_Text healthtext;

    void Start()
    {
        health = maxhealth;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movementDirection = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        aoeRadiusGFX.transform.position = mousePosition;
    }

    void FixedUpdate() {
        rb.velocity = movementDirection * movementSpeed;
        float percentage = (float)health/(float)maxhealth*100;
        healthtext.SetText(percentage + "%");
    }
}
