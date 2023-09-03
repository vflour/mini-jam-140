using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandLogic : MonoBehaviour
{
    private aoeLogic wandGFX;
    public GameObject[] wandTypeGFX;

    public float cooldown = 0;
    public GameObject[] summonTypeGFX;
    private GameObject aoeRadiusGFX;
    public AudioSource spellSFX;

    public void EquipWand(int wand)
    {
        Debug.Log("Trying to equip " + wand);
        // incase dequipped
        if (wand > wandTypeGFX.Length && wand < -1)
            return;
        
        // remove old
        if (wandGFX != null)
            Destroy(wandGFX.gameObject);
        if (aoeRadiusGFX != null)
            Destroy(aoeRadiusGFX);

        var newGFX = Instantiate(wandTypeGFX[wand], transform);
        wandGFX = newGFX.GetComponent<aoeLogic>(); 
        aoeRadiusGFX = Instantiate(summonTypeGFX[wand], transform);
        
    }

    void Update()
    {   
        var camera = Camera.main;
        if (wandGFX == null) return;
        
        aoeRadiusGFX.SetActive(cooldown > 0);
        aoeRadiusGFX.transform.position = transform.position;

        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
            return;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            // get direction of mouse
            var direction = camera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            direction.Normalize();
            var rot = Mathf.Atan2(-direction.x, direction.y) * Mathf.Rad2Deg + 180; 
            
            // apply pos and rot to gfx
            wandGFX.transform.position = transform.position;
            wandGFX.transform.rotation = Quaternion.Euler(0, 0, rot);
            spellSFX.Play();

            cooldown = 0.5f;
            wandGFX.ToggleEnabled(true); 
            
        }
    }
}
