using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<Item> Items = new List<Item>();

    public Transform ItemContent;
    public GameObject InventoryItem;

    public Toggle EnableRemove;

    public InventoryItemController[] InventoryItems;

    private void Awake()
    {
        Instance = this;
    }

    public void Add(Item item)
    {
        foreach(var existingItem in Items)
        {
            if(existingItem.id == item.id)
            {
                existingItem.quantity++;
                return;
            }
        }
        Items.Add(item);
        item.quantity = 1;
    }
    public void Remove(Item item)
    {
        if(item.quantity > 1) item.quantity--;
        else 
        {
            item.quantity--;
            Items.Remove(item);
        }
        UpdateQuantities();
    }
    public void ListItems()
    {
        CleanList();
        foreach(var item in Items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<TMP_Text>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            var removeButton = obj.transform.Find("RemoveButton").GetComponent<Button>();
            var quantityText = obj.transform.Find("QuantityText").GetComponent<TMP_Text>();
       
            itemName.text = item.itemName;
            itemIcon.sprite = item.icon;
            quantityText.text = item.quantity.ToString();

            if(EnableRemove.isOn)
            {
                removeButton.gameObject.SetActive(true);
            }
       }

       SetInventoryItems();
    }

    public void CleanList()
    {
        foreach(Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }
    }
    public void UpdateQuantities()
    {
        foreach(Transform item in ItemContent)
        {
            item.Find("QuantityText").GetComponent<TMP_Text>().text = item.gameObject.GetComponent<InventoryItemController>().item.quantity.ToString();
            //za stakovanje: umesto u Item skriptu treba stavim quantity u InventorItemController verovatno
        }
    }

    public void EnableItemsRemove()
    {
        if(EnableRemove.isOn)
        {
            foreach(Transform item in ItemContent)
            {
                item.Find("RemoveButton").gameObject.SetActive(true);
            }
        }
        else
        {
            foreach(Transform item in ItemContent)
            {
                item.Find("RemoveButton").gameObject.SetActive(false);
            }
        }
    }

    public void SetInventoryItems()
    {
        InventoryItems = ItemContent.GetComponentsInChildren<InventoryItemController>();
        for(int i=0;i<Items.Count;i++)
        {
            InventoryItems[i].AddItem(Items[i]);
        }
    }
}
