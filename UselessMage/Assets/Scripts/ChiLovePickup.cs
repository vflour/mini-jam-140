using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChiLovePickup : LovePickup
{
    public override void FinishAnimation()
    {

        GameData.Instance.watchGameWinScene = true;
        SceneManager.LoadScene("hub");
            
    }
}
