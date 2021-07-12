using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBase
{

}

public interface IInteractable : IBase
{
    void Interact();
    void EndInteraction(bool sucess);
}

public class ItemObject : MonoBehaviour, IInteractable
{
    [SerializeField] private ItemInfo item = null;

    public ItemInfo GetItem() => item;

    public void SetItem(ItemInfo item) => this.item = item;

    public void Interact()
    {
        InventoryManager.Instance.ShowInventory(item);

        InventoryManager.Instance.onItemAdded += EndInteraction;
    }

    public void EndInteraction(bool sucess)
    {
        InventoryManager.Instance.onItemAdded -= EndInteraction;

        if (sucess)
        {
            Destroy(gameObject);
        }
    }
}

[System.Serializable]
public class ItemInfo
{
    [SerializeField] private Item item = default;
    [SerializeField] private int amount = 1;

    public ItemInfo(Item item, int amount)
    {
        this.item = item;
        this.amount = amount;
    }

    public Item Item { get => item; set => item = value;  }
    public int Amount { get => amount; set => amount = value; }
}
