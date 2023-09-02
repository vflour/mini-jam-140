using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandLogic : MonoBehaviour
{
    public aoeLogic wandGFX;
    public float cooldown = 0;
    public GameObject aoeRadiusGFX;

    // Update is called once per frame
    void Update()
    {           
        var camera = Camera.main;
        
        aoeRadiusGFX.SetActive(cooldown > 0);
        aoeRadiusGFX.transform.position = transform.position;
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
            return;
        }
        
        if (Input.GetButtonDown("Fire1"))
        {
            var direction = camera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            direction.Normalize();
            var rot = Mathf.Atan2(-direction.x, direction.y) * Mathf.Rad2Deg + 180; 
            
            wandGFX.transform.position = transform.position;
            wandGFX.transform.rotation = Quaternion.Euler(0, 0, rot);

            cooldown = 0.5f;

            wandGFX.ToggleEnabled(true); 
            
        }
    }
}
