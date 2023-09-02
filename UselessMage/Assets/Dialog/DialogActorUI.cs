using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static DialogChain;

public class DialogActorUI : MonoBehaviour
{
    public GameObject panel;
    public Image actorSprite;
    public TMP_Text actorName;
    public Color actorSpeakingColor;
    public Color actorNotSpeakingColor;
    public Color nameSpeakingColor;
    public Color nameNotSpeakingColor;
    

    private DialogActor currentDialogActorData;

    public void SetData(DialogActor dialogActorData)
    {
        currentDialogActorData = dialogActorData;
        if (dialogActorData.setName)
        {
            actorName.text = dialogActorData.name;
        }
        if (dialogActorData.setPortrait)
        {
            actorSprite.sprite = dialogActorData.portrait;
        }
        if (actorSprite != null)
        {
            actorSprite.color = dialogActorData.isSpeaking ? actorSpeakingColor : actorNotSpeakingColor;
            actorName.color = dialogActorData.isSpeaking ? nameSpeakingColor : nameNotSpeakingColor;
        }
        panel.SetActive(actorSprite.sprite != null);
    }

}
