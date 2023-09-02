using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudItemUI : MonoBehaviour
{

    public int id = -1;
    public Button button;
    public GameObject selected;

    public void SetSelected(bool setSelected = true)
    {
        selected.SetActive(setSelected);
    }

    public bool IsItemEnabled()
    {
        return gameObject.activeSelf;
    }

}
