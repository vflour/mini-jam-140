using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static DialogChain;

public class DialogUI : MonoBehaviour
{
    public DialogActorUI dialogActorUILeft;
    public DialogActorUI dialogActorUIRight;
    public TMP_Text dialogText;
    public Button nextButton;
    public Button closeButton;

    public AudioSource dialogAudioSource;

    DialogItem currentDialogItem;
    private float dialogTextTime;
    private float dialogTextDeltaTime;
    private int currentVariantIndex;

    public void Update()
    {
        dialogTextDeltaTime += Time.deltaTime;
        if (dialogTextDeltaTime > dialogTextTime)
        {
            dialogTextDeltaTime = dialogTextTime;
        }
        string line = GetCurrentLine();
        float percent = dialogTextDeltaTime / dialogTextTime;
        dialogText.text = line.Substring(0, Mathf.RoundToInt(line.Length * percent));
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private string GetCurrentLine()
    {
        return currentDialogItem.lineVariants[currentVariantIndex].lineString;
    }

    public void SetData(DialogItem dialogItem, bool lastLine, int variant)
    {
        currentDialogItem = dialogItem;
        dialogTextDeltaTime = 0;
        dialogActorUILeft.SetData(dialogItem.leftActor);
        dialogActorUIRight.SetData(dialogItem.rightActor);
        dialogText.text = string.Empty;
        currentVariantIndex = variant % dialogItem.lineVariants.Count;
        if (dialogItem.lineVariants[currentVariantIndex].lineClip)
        {
            dialogAudioSource.Stop();
            dialogAudioSource.clip = dialogItem.lineVariants[currentVariantIndex].lineClip;
            dialogAudioSource.Play();
            dialogTextTime = dialogAudioSource.clip.length;
        }
        else
        {
            dialogTextTime = 1;
        }
        nextButton.gameObject.SetActive(!lastLine);
        closeButton.gameObject.SetActive(lastLine);
    }

}
