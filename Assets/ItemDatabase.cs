using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Database", menuName = "Create/Database/New Item Database")]
public class ItemDatabase : ScriptableObject
{
    public ItemObject[] items;

    [ContextMenu("Sync Items")]
    public void SyncItems() {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].data.itemID != i)
            {
                items[i].data.itemID = i;
            }
        }
    }
}
