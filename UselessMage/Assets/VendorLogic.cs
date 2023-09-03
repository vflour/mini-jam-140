using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendorLogic : MonoBehaviour
{
    public int WandNumber;
    public int WandPrice;
    private void OnTriggerEnter2D(Collider2D other)
    {
       
        if(other.tag == "Player"){
            if(GameData.Instance.collectedWands[WandNumber] == false &&
            GameData.Instance.currency >= WandPrice){
                GameData.Instance.collectedWands[WandNumber] = true;
                Debug.Log("Bought wand " + WandNumber);
                GameData.Instance.currency -= WandPrice;
            }
        }
    }
}
