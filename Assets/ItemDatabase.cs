using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Database", menuName = "Create/Database/New Item Database")]
public class ItemDatabase : ScriptableObject
{
    public List<Item> items;

    [ContextMenu("Sync Items")]
    public void SyncItems() {
        for (int i = 0; i < items.Count; i++)
        {
            items[i].itemID = i;
        }
    }
}
