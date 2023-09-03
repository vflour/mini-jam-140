using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPortalLogic : MonoBehaviour
{
    public GameObject ActivePortal;
    public GameObject InactivePortal;

    // Update is called once per frame
    void Update()
    {
        if(GameData.Instance.collectedWands[3] == true){
            InactivePortal.SetActive(false);
            ActivePortal.SetActive(true);
        }
    }
}
