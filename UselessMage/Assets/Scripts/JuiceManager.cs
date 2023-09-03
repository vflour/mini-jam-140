using System;
using UnityEngine;

public class JuiceManager : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip clickClip;
    public AudioClip enterClip;

    public void Start()
    {
        foreach (JuiceButton button in FindObjectsOfType<JuiceButton>(true))
        {
            button.onButtonEnter.AddListener(OnButtonEnter);
            button.onButtonClick.AddListener(OnButtonClick);
        }
    }

    private void OnButtonEnter()
    {
        audioSource.Stop();
        audioSource.clip = enterClip;
        audioSource.Play();
    }

    private void OnButtonClick()
    {
        audioSource.Stop();
        audioSource.clip = clickClip;
        audioSource.Play();
    }
}
