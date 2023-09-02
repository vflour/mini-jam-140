using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public string NextLevelName;

    private void OnTriggerEnter2D(Collider2D other)
    {
       
        if(other.tag == "Player"){
            Debug.Log("Setting level to " + NextLevelName);
            GameData.Instance.CurrentLevel = NextLevelName;
            SceneManager.LoadScene ("game");
        }
    }
}
