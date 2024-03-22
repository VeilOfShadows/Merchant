using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;

public class UserInterface : MonoBehaviour
{
    public Canvas canvas;
    public Tween growTween;
    public Inventory inventory;
    public Inventory syncedInventory;
    public GameObject inventoryPrefab;
    public Dictionary<GameObject, InventorySlot> slotsOnInterface = new Dictionary<GameObject, InventorySlot>();
    public bool inShop;
    public MerchantInventoryInterface merchantInterface;
    public UserInterface playerInterface;
    public TooltipManager tooltipManager;
    public float tooltipOffset;

    public virtual void Awake()
    {
        for (int i = 0; i < inventory.slots.Length; i++)
        {
            inventory.slots[i].parent = this;
            inventory.slots[i].OnAfterUpdate += OnSlotUpdate;
        }
        CreateSlots();
        AddEvent(gameObject, EventTriggerType.PointerEnter, delegate { OnEnterInterface(gameObject); });
        AddEvent(gameObject, EventTriggerType.PointerExit, delegate { OnExitInterface(gameObject); });
    }

    public void CreateSlots() {
        slotsOnInterface = new Dictionary<GameObject, InventorySlot>();
        for (int i = 0; i < inventory.slots.Length; i++)
        {
            var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            //obj.GetComponent<RectTransform>().localPosition = GetPosition(i);

            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerClick, delegate { OnClick(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
            AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
            AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
            AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });
            //AddEvent(obj, EventTriggerType.PointerEnter, delegate { EnterSlot(obj); });
            //AddEvent(obj, EventTriggerType.PointerExit, delegate { ExitSlot(obj); });

            inventory.slots[i].slotDisplay = obj;

            slotsOnInterface.Add(obj, inventory.slots[i]);
            //OnSlotUpdate(slotsOnInterface[obj]);
        }
        slotsOnInterface.UpdateSlotDisplay();
    }

    [ContextMenu("SyncNew")]
    public void SyncNew(Inventory newInventory) 
    {
        //if (newInventory == syncedInventory)
        //{
        //    return;
        //}

        syncedInventory = newInventory;

        inventory.Clear();

        //if (inventory.slots.Length < 0)
        //{
        //    return;
        //}

        for (int i = 0; i < inventory.slots.Length; i++)
        {
            inventory.slots[i].slotDisplay.SetActive(false);
        }

        //for (int i = 0; i < syncedInventory.slots.Length; i++)
        //{
        //    inventory.slots[i].item = syncedInventory.slots[i].item;
        //    inventory.slots[i].amount = syncedInventory.slots[i].amount;
        //    inventory.slots[i].slotDisplay.SetActive(true);
        //}

        for (int i = 0; i < syncedInventory.slots.Length; i++)
        {
            slotsOnInterface[inventory.slots[i].slotDisplay] = syncedInventory.slots[i];
            slotsOnInterface[inventory.slots[i].slotDisplay].slotDisplay = syncedInventory.slots[i].slotDisplay;
            //if (syncedInventory.slots[i].item != inventory.slots[i].item)
            //{
            //    inventory.slots[i].item = syncedInventory.slots[i].item;
            //}

            //if (syncedInventory.slots[i].amount != inventory.slots[i].amount)
            //{
            //    inventory.slots[i].amount = syncedInventory.slots[i].amount;
            //}
           
            inventory.slots[i].item = syncedInventory.slots[i].item;
            inventory.slots[i].amount = syncedInventory.slots[i].amount;
            inventory.slots[i].slotDisplay.SetActive(true);
        }
        //inventory = syncedInventory;
        slotsOnInterface.UpdateSlotDisplay();
    }

    public void SyncWithInventory() {
        if (syncedInventory == null)
        {
            return;
        }

        for (int i = 0; i < syncedInventory.slots.Length; i++)
        {
            if (syncedInventory.slots[i].item != inventory.slots[i].item)
            {
                inventory.slots[i].item = syncedInventory.slots[i].item;
            }

            if (syncedInventory.slots[i].amount != inventory.slots[i].amount)
            {
                inventory.slots[i].amount = syncedInventory.slots[i].amount;
            }

            if (syncedInventory.slots[i].slotWeight != inventory.slots[i].slotWeight)
            {
                inventory.slots[i].slotWeight = syncedInventory.slots[i].slotWeight;
            }
            //inventory.slots[i].slotDisplay.SetActive(true);
        }

        slotsOnInterface.UpdateSlotDisplay();
    }

    private void OnSlotUpdate(InventorySlot _slot)
    {
        _slot.slotDisplay.transform.GetChild(2).gameObject.SetActive(false);
        _slot.slotDisplay.transform.GetChild(3).gameObject.SetActive(false);
        if (_slot.item == null)
        {
            _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
            _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
            _slot.slotDisplay.GetComponentInChildren<TextMeshProUGUI>().text = "";
        }
        else
        {
            if (_slot.item.itemID >= 0)
            {
                _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().sprite = _slot.item.uiDisplay;
                if (_slot.item.itemType == ItemType.QuestItem)
                {
                    _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, .8f, 0, 1);
                }
                else
                {
                    _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                }
                //_slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                _slot.slotDisplay.GetComponentInChildren<TextMeshProUGUI>().text = _slot.amount == 1 ? "" : _slot.amount.ToString("n0");

                if (MouseData.interfaceMouseIsOver == merchantInterface)
                {
                    if (PriceManager.instance.CheckIfPriceIsModified(_slot.item))
                    {
                        if (PriceManager.instance.CheckHigherOrLower(_slot.item))
                        {
                            _slot.slotDisplay.transform.GetChild(3).gameObject.SetActive(true);
                        }
                        else
                        {
                            _slot.slotDisplay.transform.GetChild(2).gameObject.SetActive(true);
                        }
                    }
                }
                else
                {
                    if (PriceManager.instance.CheckIfPriceIsModified(_slot.item))
                    {
                        if (PriceManager.instance.CheckHigherOrLower(_slot.item))
                        {
                            _slot.slotDisplay.transform.GetChild(2).gameObject.SetActive(true);
                        }
                        else
                        {
                            _slot.slotDisplay.transform.GetChild(3).gameObject.SetActive(true);
                        }
                    }
                }

            }
            else
            {
                _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
                _slot.slotDisplay.GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
        }
    }

    protected void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }

    //private Vector3 GetPosition(int i)
    //{
    //    //return new Vector3(X_START + (X_SPACE_BETWEEN_ITEM * (i % NUMBER_OF_COLUMN)), Y_START + (-Y_SPACE_BETWEEN_ITEMS * (i / NUMBER_OF_COLUMN)), 0f);
    //}

    public virtual void OnEnter(GameObject obj)
    {
        MouseData.slotHoveredOver = obj;
        growTween.Complete();
        growTween = obj.transform.DOScale(1.2f, .3f);
        if (slotsOnInterface[obj].item != null)
        {
            if (slotsOnInterface[obj].item.itemID != -1)
            {
                tooltipManager.ShowTooltip(slotsOnInterface[obj].item, tooltipOffset, true);                
            }
        }
    }

    public virtual void OnClick(GameObject obj)
    {
        if (inShop)
        {
            if (slotsOnInterface[obj].item.itemType == ItemType.QuestItem)
            {
                NotificationManager.instance.DisplayNotification("Cannot sell quest items.", true, 1.4f);
            }
            else
            {
                int buyPrice = slotsOnInterface[obj].item.baseCoinValue;
                if (merchantInterface.syncedInventory.AttemptPurchase(slotsOnInterface[obj].item, buyPrice))
                {
                    slotsOnInterface[obj].RemoveAmount(1);
                    syncedInventory.AddItem(syncedInventory.coinItem.data, buyPrice);
                }
                else
                {
                    NotificationManager.instance.DisplayNotification("Vendor does not have enough gold.", true, 1.4f);
                }
            }
            SyncWithInventory();
            //slotsOnInterface.UpdateSlotDisplay();
        }
        else
        {
            if (slotsOnInterface[obj].item.itemType == ItemType.Consumable)
            {
                PlayerHungerManager.instance.EatSelectedFood(slotsOnInterface[obj]);
            }
        }
        //MouseData.slotHoveredOver = obj;
    }
    public void OnExit(GameObject obj)
    {
        MouseData.slotHoveredOver = null;
        growTween.Complete();
        growTween = obj.transform.DOScale(1, .3f);
        tooltipManager.HideTooltip();
    }

    public void OnEnterInterface(GameObject obj)
    {
        MouseData.interfaceMouseIsOver = obj.GetComponent<UserInterface>();
    }
    public void OnExitInterface(GameObject obj)
    {
        MouseData.interfaceMouseIsOver = null;
    }

    public GameObject CreateTempItem(GameObject obj)
    {
        GameObject tempItem = null;
        if (slotsOnInterface[obj].item.itemID >= 0)
        {
            tempItem = new GameObject();
            var rt = tempItem.AddComponent<RectTransform>();
            rt.sizeDelta = new Vector2(50, 50);
            tempItem.transform.SetParent(transform.parent);
            var img = tempItem.AddComponent<Image>();
            if (slotsOnInterface[obj].item.itemType == ItemType.QuestItem)
            {
                img.color = new Color(1, .8f, 0, 1);
            }
            img.sprite = slotsOnInterface[obj].item.uiDisplay;
            img.raycastTarget = false;
        }
        return tempItem;
    }

    public virtual void OnDrag(GameObject obj)
    {
        if (MouseData.tempItemBeingDragged != null)
        {
            MouseData.tempItemBeingDragged.GetComponent<RectTransform>().position = Input.mousePosition;
            //InventorySlot mouseHoverData = MouseData.interfaceMouseIsOver.slotsOnInterface[MouseData.slotHoveredOver];
            //if (MouseData.draggedItem == null)
            //{
            //    MouseData.draggedItem = mouseHoverData.ItemObject;
            //}
        }
    }

    public virtual void OnDragEnd(GameObject obj)
    {
        canvas.sortingOrder = 0;
        canvas = null;
        Destroy(MouseData.tempItemBeingDragged);
        if (MouseData.interfaceMouseIsOver == null)
        {
            //slotsOnInterface[obj].DropItem(MouseData.draggedItem, inventory.floorItem);
            //slotsOnInterface[obj].RemoveItem();
            return;
        }
        if (MouseData.interfaceMouseIsOver == this)
        {
            if (MouseData.slotHoveredOver)
            {
                InventorySlot mouseHoverSlotData = MouseData.interfaceMouseIsOver.slotsOnInterface[MouseData.slotHoveredOver];
                inventory.SwapItems(slotsOnInterface[obj], mouseHoverSlotData);
            }
        }
    }

    public virtual void OnDragStart(GameObject obj)
    {
        MouseData.tempItemBeingDragged = CreateTempItem(obj);
        //HideTooltip();
        //MouseData.startInterface = MouseData.interfaceMouseIsOver.type;

        transform.GetComponentInParent<Canvas>().sortingOrder = 10;
        canvas = transform.GetComponentInParent<Canvas>();
    }
    //public void OnSlotUpdate(InventorySlot _slot)
    //{
    //    if (Application.isPlaying)
    //    {
    //        if (_slot.item.itemID >= 0)
    //        {
    //            _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().sprite = _slot.item.uiDisplay;
    //            _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
    //            _slot.slotDisplay.GetComponentInChildren<TextMeshProUGUI>().text = _slot.amount == 1 ? "" : _slot.amount.ToString("n0");
    //        }
    //        else
    //        {
    //            _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
    //            _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
    //            _slot.slotDisplay.GetComponentInChildren<TextMeshProUGUI>().text = "";
    //        }
    //    }
    //}
}
 public static class MouseData
    {
        public static UserInterface interfaceMouseIsOver;
        public static GameObject tempItemBeingDragged;
        public static GameObject slotHoveredOver;
        public static Item draggedItem;
    }

public static class ExtensionMethods
{
    public static void UpdateSlotDisplay(this Dictionary<GameObject, InventorySlot> _slotsOnInterface)
    {
        foreach (KeyValuePair<GameObject, InventorySlot> _slot in _slotsOnInterface)
        {
             _slot.Key.transform.GetChild(2).gameObject.SetActive(false);
             _slot.Key.transform.GetChild(3).gameObject.SetActive(false);
            if (_slot.Value.item == null)
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
            else
            {
                if (_slot.Value.item.itemID >= 0)
                {
                    _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = _slot.Value.item.uiDisplay;
                    if (_slot.Value.item.itemType == ItemType.QuestItem)
                    {
                        _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, .8f, 0, 1);
                    }
                    else
                    {
                        _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                    }
                    _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = _slot.Value.amount == 1 ? "" : _slot.Value.amount.ToString("n0");
                    
                    
                    
                    if (PriceManager.instance.CheckIfPriceIsModified(_slot.Value.item))
                    {
                        if (PriceManager.instance.CheckHigherOrLower(_slot.Value.item))
                        {
                            _slot.Key.transform.GetChild(2).gameObject.SetActive(true);
                        }
                        else
                        {
                            _slot.Key.transform.GetChild(3).gameObject.SetActive(true);
                        }
                    }
                }
                else
                {
                    _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                    _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
                    _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = "";
                }
            }
        }
    }
}