using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

[System.Serializable]
public struct ItemPriceMultiplier
{
    public ItemObject item;
    public float priceMultiplierPercent;
}

[CreateAssetMenu(fileName = "Inventory", menuName = "Create/Inventory/New Inventory")]
public class Inventory : ScriptableObject
{
    public ItemDatabase database;
    public InventoryClass container;
    public float totalWeight;
    public InventorySlot[] slots { get { return container.Slots; } }
    public ItemObject coinItem;
    [SerializeField]
    public ItemPriceMultiplier[] stockItems;
    public ItemPriceMultiplier[] playerItems;

    public virtual void AddItem(Item _item, int _amount) {
        InventorySlot slot = FindItemInInventory(_item);

        if (_item.itemType == ItemType.QuestItem)
        {
            if (slot == null)
            {
                SetEmptySlot(_item, _amount);
                return;
                //return true;
            }
        }
        else if (!database.items[_item.itemID].data.stackable || slot == null)
        {
            SetEmptySlot(_item, _amount);
            return;
            //return true;
        }
        slot.AddAmount(_amount);
        EvaluateWeight();
        if (PlayerManager.instance != null)
        {
            PlayerManager.instance.playerInventoryUI.SyncWithInventory();
        }
        
        //PlayerManager.instance.playerInventoryUI.SyncWithInventory();
        //return true;
    }

    public bool AttemptPurchase(Item _item, int buyPrice)
    {
        if (!_item.tradeable)
        {
            return false;
        }

        InventorySlot temp = FindItemInInventory(coinItem.data);
        if (temp == null)
        {
            Debug.Log("No coin");
            return false;
        }
        else if (temp.amount < buyPrice)
        {
            Debug.Log("Not enough coin");
            return false;
        }

        temp.RemoveAmount(buyPrice);
        AddItem(_item, 1);
        return true;
    }

    

    public int AdjustedTradePrice(Item _item) {
        for (int i = 0; i < stockItems.Length; i++)
        {
            if (stockItems[i].item.data == _item)
            {
                int temp = _item.baseCoinValue;
                Debug.Log("Base Cost: " + _item.baseCoinValue);
                Debug.Log("Multiplier: " + (stockItems[i].priceMultiplierPercent + 100) / 100);
                Debug.Log("Adjusted: " + (temp * (stockItems[i].priceMultiplierPercent + 100) / 100));
                temp = Mathf.RoundToInt(temp * ((stockItems[i].priceMultiplierPercent + 100) / 100));
                return temp;
            }
        }
        return _item.baseCoinValue;
    }

    public void EvaluateWeight() { 
        totalWeight = 0;
        for (int i = 0; i < container.Slots.Length; i++)
        {
            if (container.Slots[i].item != null)
            {
                totalWeight += container.Slots[i].slotWeight;

            }
        }
    }

    public InventorySlot SetEmptySlot(Item _item, int _amount)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].UpdateSlot(_item, _amount);
                return slots[i];
            }
            else if (slots[i].item.itemID <= -1)
            {
                slots[i].UpdateSlot(_item, _amount);
                return slots[i];
            }
        }
        EvaluateWeight();

        return null;
    }

    public InventorySlot FindItemInInventory(Item _item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                if (slots[i].item.itemID == _item.itemID)
                {
                    return slots[i];
                }
            }
        }
        return null;
    }

    public bool CheckForItem(Item _item, int _amount)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                if (slots[i].item.itemID == _item.itemID)
                {
                    if (slots[i].amount >= _amount)
                    {
                        return true;                        
                    }
                }
            }
        }
        return false;
    }

    public InventorySlot FindFoodInInventory()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item.itemType == ItemType.Consumable)
            {
                return slots[i];
            }
        }
        return null;
    }

    public void SwapItems(InventorySlot item1, InventorySlot item2)
    {
        if (item1.item.itemID == -1)
        {
            return;
        }

        if (item1.item.itemID != item2.item.itemID)
        {
            //if (item2.CanPlaceInSlot(item1.ItemObject) && item1.CanPlaceInSlot(item2.ItemObject))
            //{
                InventorySlot temp = new InventorySlot(item2.item, item2.amount);
                item2.UpdateSlot(item1.item, item1.amount);
                item1.UpdateSlot(temp.item, temp.amount);
            //}
        }
        else
        {
            if (item2.item.stackable)
            {
                if (item2 != item1)
                {
                    item2.UpdateSlot(item2.item, item1.amount + item2.amount);
                    item1.RemoveItem();
                }
            }
        }
    }

    [ContextMenu("CLEAR")]
    public void Clear()
    {
        container.Clear();
        //for (int i = 0; i < slots.Length; i++)
        //{
        //    slots[i].RemoveItem();
        //}
        EvaluateWeight();
    }

    [ContextMenu("Sync")]
    public void Sync()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].item = database.FindItem(slots[i].item.itemID);
            if (slots[i].item != null)
            {
                slots[i].SetSlotWeight();
                if (slots[i].item.itemID == -1)
                {
                    slots[i].RemoveItem();
                }
            }
        }

        EvaluateWeight();
    }
}

[System.Serializable]
public class InventoryClass
{
    public InventorySlot[] Slots = new InventorySlot[28];
    public void Clear() 
    {
        for (int i = 0; i < Slots.Length; i++)
        {
            Slots[i].RemoveItem();
            Slots[i].amount = 0;
        }
    }
}

public delegate void SlotUpdated(InventorySlot _slot);

[System.Serializable]
public class InventorySlot {
    public UserInterface parent; 
    public Item item = new Item();
    public float slotWeight;
    public int amount;
    public GameObject slotDisplay;

    [System.NonSerialized]
    public SlotUpdated OnAfterUpdate;
    [System.NonSerialized]
    public SlotUpdated OnBeforeUpdate;
    public InventorySlot()
    {
        UpdateSlot(new Item(), 0);
    }
    public InventorySlot(Item _item, int _amount)
    {
        UpdateSlot(_item, _amount);
    }

    public void UpdateSlot(Item _item, int _amount) {
        if (OnBeforeUpdate != null)
            OnBeforeUpdate.Invoke(this);
        item = _item;
        amount = _amount;
        slotWeight = item.weight * amount;
        //if (amount <= 0)
        //{
        //      RemoveItem();
        //}
        if (PlayerManager.instance != null)
        {
            PlayerManager.instance.playerInventoryUI.SyncWithInventory();
        }
        
        if (MerchantInventoryInterface.instance != null)
        {
            MerchantInventoryInterface.instance.SyncWithInventory();
        }

        if (OnAfterUpdate != null)
            OnAfterUpdate.Invoke(this);
    }

    public void AddAmount(int value)
    {
        UpdateSlot(item, amount += value);
    }

    public void RemoveAmount(int value)
    {
        if ((amount - value) <= 0)
        {
            RemoveItem();
        }
        else
        {
            UpdateSlot(item, amount -= value);
        }
    }

    public void RemoveItem()
    {
        UpdateSlot(new Item(), 0);
    }

    public void SetSlotWeight() {
        slotWeight = (item.weight * amount);
    }
}
