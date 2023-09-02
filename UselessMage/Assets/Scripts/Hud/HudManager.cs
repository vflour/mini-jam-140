using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static HudUI;

public class HudManager : MonoBehaviour
{
    public HudUI hudUI;

    public UnityEvent<int> onItemClick;

    public UnityEvent onPortraitClick;

    public bool debugResetItems;
    [Range(0, 3)]
    public int debugItemId;
    public bool debugEnableItem;
    public bool debugDisableItem;
    public bool debugSetSelectedItem;

    public void Update()
    {
        if (debugResetItems)
        {
            debugResetItems = false;
            ResetItems();
        }
        if (debugEnableItem)
        {
            debugEnableItem = false;
            EnableItem(debugItemId);
        }
        if (debugDisableItem)
        {
            debugDisableItem = false;
            DisableItem(debugItemId);
        }
        if (debugSetSelectedItem)
        {
            debugSetSelectedItem = false;
            SetSelectedItem(debugItemId);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetSelectedItem(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetSelectedItem(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetSelectedItem(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SetSelectedItem(3);
        }

    }

    public void Start()
    {
        hudUI.portraitButton.onClick.AddListener(OnPortraitClick);
        int tempId = 0;
        foreach (HudItemUI item in hudUI.items)
        {
            int tempIdScoped = tempId;
            item.button.onClick.AddListener(() => {
                OnItemButtonClick(tempIdScoped);
            });
            tempId++;
        }
        hudUI.ResetItems();
    }

    private void OnItemButtonClick(int id)
    {
        Debug.Log($"OnItemButtonClick:{id}");
        SetSelectedItem(id);
        onItemClick.Invoke(id);
    }

    private void OnPortraitClick()
    {
        Debug.Log($"OnPortraitClick");
        onPortraitClick.Invoke();
    }

    public void EnableItem(int id)
    {
        SetSelectedItem(id);
        hudUI.EnableItem(id);
    }

    public void DisableItem(int id)
    {
        hudUI.DisableItem(id);
    }

    public void SetSelectedItem(int id)
    {
        hudUI.SetSelectedItem(id);
    }

    public int GetSelectedItem()
    {
        return hudUI.GetSelectedItem();
    }

    public void ResetItems()
    {
        hudUI.ResetItems();
    }

}
