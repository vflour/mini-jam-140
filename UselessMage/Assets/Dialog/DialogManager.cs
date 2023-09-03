using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogManager : MonoBehaviour
{
    public DialogUI dialogUI;

    public DialogChain debugDialogChainScriptableObject;
    public bool debugLoad;
    private DialogChain currentDialogChain;
    private int currentIndex;
    private int currentVariant = -1;
    
    public UnityEvent onChainFinished;

    public void Start()
    {
        dialogUI.nextButton.onClick.AddListener(OnNextButtonPressed);
        dialogUI.closeButton.onClick.AddListener(Stop);
        dialogUI.skipButton.onClick.AddListener(Stop);
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
                currentDialogChain.data.Count - 1 == currentIndex,
                currentVariant
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
            IncrementVariant();
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

    public void IncrementVariant()
    {
        currentVariant++;
    }

    public void Stop()
    {
        currentDialogChain = null;
        onChainFinished.Invoke();
        dialogUI.Hide();
    }

    public bool IsDialogShowing()
    {
        return currentDialogChain != null;
    }

    public void SetVariant(int variant)
    {
        currentVariant = variant;
    }

}
