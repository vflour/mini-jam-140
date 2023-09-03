using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    // Start() and Update() methods deleted - we don't need them right now

    public static GameData Instance;

    public string CurrentLevel;
    public bool watchedIntro = false;
    public bool watchRespawnScene = false;
    public bool watchGameWinScene = false;
    public WandEquipper wandEquipper;

    public int currentVariant = 0;
    public int currency = 0;
    public bool[] collectedWands = new bool[] {true,false,false,false};

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void NewGamePlus(){
        collectedWands = new bool[] {true,false,false,false};
        currency = 0;
        watchedIntro = false;
        currentVariant++;
        if (wandEquipper)
            wandEquipper.enabled = false;
        if(currentVariant>2){
            currentVariant = 0;
        }
    }
}
