using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    public float movementSpeed;
    private Rigidbody2D rb;
    private Vector2 movementDirection;
    public GameObject aoeRadiusGFX;

    void Start()
    {
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
    }
}
