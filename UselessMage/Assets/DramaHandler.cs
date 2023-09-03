using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DramaHandler : MonoBehaviour
{
    public DialogManager dramaManager;
    public DialogChain introChain;
    public DialogChain respawnChain;
    public DialogChain gameWinChain;

    void Update()
    {
        if(!GameData.Instance.watchedIntro){
            GameData.Instance.watchedIntro = true;
            dramaManager.SetVariant(GameData.Instance.currentVariant);
            dramaManager.Load(introChain);
        }

        if(GameData.Instance.watchRespawnScene){
            GameData.Instance.watchRespawnScene = false;
            dramaManager.SetVariant(GameData.Instance.currentVariant);
            dramaManager.Load(respawnChain);
        }
        
        if (GameData.Instance.watchGameWinScene)
        {
            GameData.Instance.watchGameWinScene = false;
            dramaManager.SetVariant(GameData.Instance.currentVariant);
            dramaManager.Load(gameWinChain);
            dramaManager.onChainFinished.AddListener(() => { 
                GameData.Instance.NewGamePlus();
                dramaManager.onChainFinished.RemoveAllListeners();
            });
        }

    }

}
