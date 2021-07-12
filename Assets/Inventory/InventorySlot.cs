using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : Button
{
    public delegate void ItemSetted(Item item);
    public ItemSetted onItemSetted;

    [SerializeField] private Image itemAmountImage = null;
    [SerializeField] private Text itemAmountText = null;
    [SerializeField] private Image itemImage = null;

    [SerializeField] private CanvasGroup canvasGroup = default;

    public void Initialize()
    {
        ItemInfo = itemInfo;
        IsUnlocked = isUnlocked;
        canvasGroup = GetComponent<CanvasGroup>();
    }

    [SerializeField] private ItemInfo itemInfo = null;
    public ItemInfo ItemInfo
    {
        get => itemInfo;
        set
        {
            itemInfo = value;

            if (itemInfo.Item != null)
            {
                if (itemInfo.Item is StackableItem)
                {
                    itemAmountImage.enabled = true;
                    itemAmountText.text = itemInfo.Amount.ToString();
                }
                //itemImage.sprite = item.GetSprite();
                itemImage.enabled = true;
            }
            else
            {
                itemAmountImage.enabled = false;
                itemAmountText.text = "";
                //itemImage.sprite = null;
                itemImage.enabled = false;
            }
        }
    }

    [SerializeField] private bool isUnlocked = false;
    public bool IsUnlocked
    {
        get => isUnlocked;

        set
        {
            isUnlocked = value;
            canvasGroup.alpha = isUnlocked ? 1 : 0;
            canvasGroup.interactable = isUnlocked;
        }
    }
}