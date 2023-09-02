using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuLogic : MonoBehaviour
{
    public GameObject Credits;
    public GameObject Bestiary;

    public void toggleCredits(){
        Credits.SetActive(!Credits.activeSelf);
    }

    public void toggleBestiary(){
        Bestiary.SetActive(!Bestiary.activeSelf);
    }

    public void switchScene(string Scene){
        SceneManager.LoadScene (Scene);
    }
}
