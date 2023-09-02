using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandEquipper : MonoBehaviour
{
    private bool _enabled = false;
    void Update()
    {
        if (!_enabled)
        {
            _enabled = true;
            HudManager manager = GetComponent<HudManager>();
            var wands = GameData.Instance.collectedWands;
            for (int i = 0; i < wands.Length; i++)
            if (wands[i])
                manager.EnableItem(i);
        
    
        }
    }
}
