using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    // Start() and Update() methods deleted - we don't need them right now

    public static GameData Instance;

    public string CurrentLevel;
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

}
