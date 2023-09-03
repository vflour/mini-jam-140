using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandEquipper : MonoBehaviour
{
    public bool enabled = false;
    void Update()
    {
        if (!enabled)
        {
            enabled = true;
            HudManager manager = GetComponent<HudManager>();
            var wands = GameData.Instance.collectedWands;
            for (int i = 0; i < wands.Length; i++)
            if (wands[i])
                manager.EnableItem(i);
            else
                manager.DisableItem(i);
    
        }
    }
}
