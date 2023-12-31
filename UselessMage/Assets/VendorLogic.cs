using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendorLogic : MonoBehaviour
{
    public int WandNumber;
    public int WandPrice;
    public DialogManager dramaManager;
    public DialogChain dramaChain;

    public AudioSource buySFX;
    public WandEquipper wandEquipper;

    private void OnTriggerEnter2D(Collider2D other)
    {
       
        if(other.tag == "Player"){
            if(GameData.Instance.collectedWands[WandNumber] == false &&
            GameData.Instance.collectedWands[WandNumber-1] == true &&
            GameData.Instance.currency >= WandPrice){
                GameData.Instance.collectedWands[WandNumber] = true;
                wandEquipper.enabled = false;
                Debug.Log("Bought wand " + WandNumber);
                GameData.Instance.currency -= WandPrice;
                dramaManager.SetVariant(GameData.Instance.currentVariant);
                dramaManager.Load(dramaChain);
                buySFX.Play();
            }
        }
    }
}
