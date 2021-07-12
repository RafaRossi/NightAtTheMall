using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : Manager<InventoryManager>
{
    private List<InventorySlot> slots = new List<InventorySlot>();
    private List<ItemInfo> items = new List<ItemInfo>();
    //private Dictionary<Item, int> items = new Dictionary<Item, int>();

    [SerializeField] private CanvasGroup canvasGroup = default;
    [SerializeField] private GameObject inventoryPanel = default;
    [SerializeField] private GameObject panel = default;
    [SerializeField] private ItemConfirmationPanel itemConfirmationPanel = default;

    public delegate void ItemAdded(bool sucess);
    public ItemAdded onItemAdded = delegate { };

    public delegate void UpdateSlots();
    public UpdateSlots updateSlots = delegate { };

    public delegate void CloseInventory();
    public CloseInventory onCloseInventory = delegate { };

    [SerializeField] private int inventoryCapacity = 4;

    protected override void Initialize()
    {
        base.Initialize();
        slots.Clear();

        foreach(Transform t in panel.transform)
        {
            InventorySlot slot = t.GetComponent<InventorySlot>();

            if(slot != null)
            {
                slots.Add(slot);
                slot.Initialize();
            }
        }

        SetInventoryCapacity(inventoryCapacity);
    }

    private void Update()
    {
        if(Input.GetButtonDown("Inventory"))
        {
            if(inventoryPanel.activeSelf)
            {
                HideInventory();
            }
            else
            {
                ShowInventory();

                updateSlots.Invoke();
            }
        }
    }

    public void ShowInventory(ItemInfo item = null)
    {
        inventoryPanel.SetActive(true);
        canvasGroup.interactable = true;

        if (item != null)
        {
            itemConfirmationPanel.LoadConfirmationPanel(item);
        }

        GameManager.PauseGame();
    }

    public void HideInventory(float delay = 0)
    {
        StartCoroutine(OnHideInventory(delay));
    }

    private IEnumerator OnHideInventory(float delay = 0)
    {
        canvasGroup.interactable = false;
        yield return new WaitForSecondsRealtime(delay);
        inventoryPanel.SetActive(false);

        GameManager.ResumeGame();
        onCloseInventory.Invoke();
    }

    public void SetInventoryCapacity(int value)
    {
        for(int i = 0; i < value; i++)
        {
            slots[i].IsUnlocked = true;
        }
    }

    public int GetInventoryCapacity()
    {
        return inventoryCapacity;
        /*int amount = 0;

        foreach(InventorySlot slot in slots)
        {
            if(slot.IsUnlocked) amount += 1;
        }
        return amount;*/
    }

    public void AddInventoryCapacity(int capacity)
    {
        inventoryCapacity += capacity;

        SetInventoryCapacity(inventoryCapacity);
        /*foreach (InventorySlot slot in slots)
        {
            if (!slot.IsUnlocked)
            {
                slot.IsUnlocked = true;
                capacity -= 1;
            }

            if(capacity == 0)
            {
                break;
            }
        }*/
    }

    public bool AddItemToInventory(ItemInfo item)
    {
        if (CheckItem(item.Item) != null)
        {
            if (item.Item is StackableItem)
            {
                AddAmount(item.Item, item.Amount);
                updateSlots.Invoke();
                return true;
            }
        }

        if(items.Count < GetInventoryCapacity())
        {
            items.Add(item);
            GetSlot().ItemInfo = item;
            updateSlots.Invoke();
            return true;
        }
        return false;
    }

    private void AddAmount(Item item, int amount)
    {
        CheckItem(item).Amount += amount;
    }

    public bool RemoveItemFromInventory(Item item, int amount)
    {
        ItemInfo info = CheckItem(item);

        if (info != null)
        {
            //int a = amount == -1 ? info.Amount : amount;

            if(RemoveAmount(info.Item, amount))
            {
                if (info.Amount == 0)
                {
                    items.Remove(info);
                    GetSlot(item).ItemInfo = null;
                }
                updateSlots.Invoke();
                return true;
            }
        }
        return false;
    }

    private bool RemoveAmount(Item item, int amount)
    {
        ItemInfo info = CheckItem(item);

        if(info.Amount >= amount)
        {
            info.Amount -= amount;
            return true;
        }
        return false;
        /*if(items[item] >= amount)
        {
            items[item] -= amount;
            return true;
        }
        return false;*/
    }


    private ItemInfo CheckItem(Item _item)
    {
        return items.Find(item => item.Item.Equals(_item));
        /*foreach(Item key in items.Keys)
        {
            if (key.Equals(item))
            {
                return key;
            }
        }
        return null;*/
    }

    private InventorySlot GetSlot(Item item = null)
    {
        foreach (InventorySlot slot in slots)
        {
            if (slot.IsUnlocked && slot.ItemInfo.Item == item)
            {
                return slot;
            }
        }
        return null;
    }

    public int GetItemAmount(Item item) => CheckItem(item).Amount;
}
