using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudUI : MonoBehaviour
{
    public GameObject portraitPanel;
    public Button portraitButton;

    public List<HudItemUI> items;

    private int selectedItemId = 0;

    public void ResetItems()
    {
        selectedItemId = 0;
        foreach (HudItemUI item in items)
        {
            DisableItem(item);
            item.SetSelected(false);
        }
    }

    public void EnableItem(int id)
    {
        foreach (HudItemUI item in items)
        {
            if (item.id == id)
            {
                items[id].gameObject.SetActive(true);
            }
        }
    }

    public void DisableItem(int id)
    {
        foreach (HudItemUI item in items)
        {
            if (item.id == id)
            {
                DisableItem(item);
            }
        }
    }

    public bool IsItemEnabled(int id)
    {
        foreach (HudItemUI item in items)
        {
            if (item.id == id)
            {
                return items[id].IsItemEnabled();
            }
        }
        return false;
    }

    private void DisableItem(HudItemUI item)
    {
        item.gameObject.SetActive(false);
    }

    public void SetSelectedItem(int id)
    {
        foreach (HudItemUI item in items)
        {
            item.SetSelected(item.id == id);
        }
    }

    public int GetSelectedItem()
    {
        return selectedItemId;
    }



}
