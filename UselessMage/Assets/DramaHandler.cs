using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DramaHandler : MonoBehaviour
{
    public DialogManager dramaManager;
    public DialogChain introChain;
    public DialogChain respawnChain;

    void Update()
    {
        if(!GameData.Instance.watchedIntro){
            GameData.Instance.watchedIntro = true;
            dramaManager.Load(introChain);
        }

         if(GameData.Instance.watchRespawnScene){
            GameData.Instance.watchRespawnScene = false;
            dramaManager.Load(respawnChain);
        }
    }

}
