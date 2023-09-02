using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogChain", menuName = "ScriptableObjects/DialogChain", order = 1)]
public class DialogChain : ScriptableObject
{
    [Serializable]
    public class DialogLine
    {
        public string lineString;
        public AudioClip lineClip;
    }

    [Serializable]
    public class DialogActor
    {
        public bool setName;
        public string name;
        public bool setPortrait;
        public Sprite portrait;
        public bool isSpeaking;
    }

    [Serializable]
    public class DialogItem
    {
        public DialogActor leftActor;
        public DialogActor rightActor;
        public List<DialogLine> lineVariants;
        
    }


    public List<DialogItem> data;
}
