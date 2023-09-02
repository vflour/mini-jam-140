using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandLogic : MonoBehaviour
{
    public aoeLogic wandGFX;
    public float cooldown = 0;

    // Update is called once per frame
    void Update()
    {           
        var camera = Camera.main;
        
        if (cooldown >= 0)
        {
            cooldown -= Time.deltaTime;
        }
        if (Input.GetButtonDown("Fire1"))
        {

            var direction = camera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            direction.Normalize();
            var rot = Mathf.Atan2(-direction.x, direction.y) * Mathf.Rad2Deg + 180; 
            
            wandGFX.transform.position = transform.position;
            wandGFX.transform.rotation = Quaternion.Euler(0, 0, rot);

            cooldown = 10;

            wandGFX.ToggleEnabled(true); 
            
        }
    }
}
