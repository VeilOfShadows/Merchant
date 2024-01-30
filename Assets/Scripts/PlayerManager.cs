using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Inventory playerInventory;
    public InventorySlot itemToAdd;

    [ContextMenu("ADD")]
    public void AddItem() {
        playerInventory.AddItem(itemToAdd.item, itemToAdd.amount);
    }
}
