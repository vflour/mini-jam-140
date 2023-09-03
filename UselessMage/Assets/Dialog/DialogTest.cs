using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogTest : MonoBehaviour
{
    public DialogManager dialogManager;

    public TMP_Dropdown targetDropdown;
    public TMP_Dropdown variantDropdown;
    public Button runButton;

    public List<DialogChain> dialogChains;

    public void Start()
    {

        List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();
        foreach (DialogChain dialogChain in dialogChains)
        {
            options.Add(new TMP_Dropdown.OptionData
            {
                text = dialogChain.name
            });
        }
        targetDropdown.options = options;

        runButton.onClick.AddListener(OnRunButtonPressed);

    }

    public DialogChain FindDialogChain(string target)
    {
        foreach (DialogChain dialogChain in dialogChains)
        {
            if (dialogChain.name == target)
            {
                return dialogChain;
            }
        }
        return null;
    }

    private void OnRunButtonPressed()
    {
        int variant = int.Parse(variantDropdown.options[variantDropdown.value].text);
        dialogManager.SetVariant(variant);

        string target = targetDropdown.options[targetDropdown.value].text;
        DialogChain dialogChain = FindDialogChain(target);
        dialogManager.Load(dialogChain);
    }
}
