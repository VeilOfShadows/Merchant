using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Database", menuName = "Create/Database/New Item Database")]
public class ItemDatabase : ScriptableObject
{
    public ItemObject[] items;

    [ContextMenu("Set Item IDs")]
    public void SetIDs() {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].data.itemID != i)
            {
                items[i].data.itemID = i;
            }
        }
    }

    public Item FindItem(ItemObject _item) {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].data.itemID == _item.data.itemID)
            {
                return items[i].data;
            }
        }
        return null;
    }
}
