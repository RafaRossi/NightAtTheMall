using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemConfirmationPanel : MonoBehaviour
{
    [SerializeField] private Text itemName = default;
    [SerializeField] private Image itemSprite = default;

    private ItemInfo itemInfo = default;

    private void Awake()
    {
        InventoryManager.Instance.onCloseInventory += HideConfirmationPanel;
    }

    public void LoadConfirmationPanel(ItemInfo itemInfo)
    {
        if(itemInfo != null)
        {
            this.itemInfo = itemInfo;

            itemName.text = itemInfo.Item.GetName();
            itemSprite.sprite = itemInfo.Item.GetSprite();

            gameObject.SetActive(true);
        }
    }

    public void HideConfirmationPanel()
    {
        gameObject.SetActive(false);
    }

    public void ConfirmButton()
    {
        bool sucess = InventoryManager.Instance.AddItemToInventory(itemInfo);

        if(sucess)
        {
            InventoryManager.Instance.HideInventory(1f);
        }
        InventoryManager.Instance.onItemAdded.Invoke(sucess);
    }

    public void CancelButton()
    {
        InventoryManager.Instance.HideInventory();
        InventoryManager.Instance.onItemAdded.Invoke(false);
    }
}
