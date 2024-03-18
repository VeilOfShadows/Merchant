using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.Progress;

[CreateAssetMenu(fileName = "Item Database", menuName = "Create/Database/New Item Database")]
public class ItemDatabase : ScriptableObject
{
    public ItemObject[] items;
    public ItemObject[] questItems;

    [ContextMenu("Set Item IDs")]
    public void SetIDs() {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].data.itemID != i)
            {
                items[i].data.itemID = i;
            }
        }

        for (int i = 0; i < questItems.Length; i++)
        {
            if (questItems[i].data.itemID != 1110 +i)
            {
                questItems[i].data.itemID = 1110 +i;
            }
        }
    }

    public Item FindItemObject(ItemObject _item) {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].data.itemID == _item.data.itemID)
            {
                return items[i].data;
            }
        }

        for (int i = 0; i < questItems.Length; i++)
        {
            if (questItems[i].data.itemID == _item.data.itemID)
            {
                return questItems[i].data;
            }
        }
        return null;
    }

    public Item FindItem(int _itemID)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].data.itemID == _itemID)
            {
                return items[i].data;
            }
        }

        for (int i = 0; i < questItems.Length; i++)
        {
            if (questItems[i].data.itemID == _itemID)
            {
                return questItems[i].data;
            }
        }
        return null;
    }
}
