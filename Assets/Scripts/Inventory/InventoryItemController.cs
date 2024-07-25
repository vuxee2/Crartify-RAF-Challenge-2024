using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour
{
    public Item item;

    public Button RemoveButton;

    public void RemoveItem()
    {
        InventoryManager.Instance.Remove(item);

        if(item.quantity <= 0) Destroy(gameObject);
    }

    public void AddItem(Item newItem)
    {
        item = newItem;
    }

    public void UseItem()
    {
        switch(item.itemType)
        {
            case Item.ItemType.Food:
                SoundManager.defPlaySound("playerEat");
                Player.UpdateHunger(Mathf.Min(100, Player.hunger + 10));
                Player.UpdateHealth(Mathf.Min(100, Player.health + 5));
                RemoveItem();
                break;
        }
        if(item.itemName == "Sapphire" && item.quantity > 0)
        {
            GameObject.FindGameObjectWithTag("Player").transform.Find("Lantern").gameObject.SetActive(true);
            InventoryManager.Instance.Remove(item);
        }
    }
}
