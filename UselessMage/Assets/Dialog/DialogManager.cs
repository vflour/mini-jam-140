using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public DialogUI dialogUI;

    public DialogChain debugDialogChainScriptableObject;
    public bool debugLoad;
    private DialogChain currentDialogChain;
    private int currentIndex;

    public void Start()
    {
        dialogUI.nextButton.onClick.AddListener(OnNextButtonPressed);
        dialogUI.closeButton.onClick.AddListener(Stop);
        dialogUI.Hide();
    }

    private void OnNextButtonPressed()
    {
        currentIndex++;
        LoadLine();
    }

    private void LoadLine()
    {
        if (currentDialogChain.data.Count > currentIndex)
        {
            dialogUI.SetData(
                currentDialogChain.data[currentIndex],
                currentDialogChain.data.Count - 1 == currentIndex
            );
        }
        else
        {
            Stop();
        }
    }

    public void Update()
    {
        if (debugLoad)
        {
            debugLoad = false;
            Load(debugDialogChainScriptableObject);
        }
    }

    public void Load(DialogChain dialogChain)
    {
        currentIndex = 0;
        currentDialogChain = dialogChain;
        dialogUI.Show();
        LoadLine();
    }

    public void Stop()
    {
        currentDialogChain = null;
        dialogUI.Hide();
    }

    public bool IsDialogShowing()
    {
        return currentDialogChain != null;
    }

}