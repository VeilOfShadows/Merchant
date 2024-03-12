using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipPositioner : MonoBehaviour
{
    public Vector2 offset;
    public RectTransform rect;
    Vector2 pos;

    private void LateUpdate()
    {
        if (offset.x < 0)
        {
            pos.x = MouseData.slotHoveredOver.transform.position.x + rect.sizeDelta.x;
        }
        else
        {
            pos.x = MouseData.slotHoveredOver.transform.position.x - rect.sizeDelta.x;
        }
        pos.y = MouseData.slotHoveredOver.transform.position.y;
        pos += offset;
        rect.position = pos;
    }
}
