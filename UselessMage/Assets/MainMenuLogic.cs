using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuLogic : MonoBehaviour
{
    public GameObject Credits;

    public void toggleCredits(){
        Credits.SetActive(!Credits.activeSelf);
    }

    public void switchScene(string Scene){
        SceneManager.LoadScene (Scene);
    }
}
