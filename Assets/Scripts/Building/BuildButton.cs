using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BuildButton : MonoBehaviour
{
    public int[] matsRequired;
    public Sprite[] matsSprites;

    public GameObject reqMat;
    public Transform reqMatContent;
    void Start()
    {
        for(int i=0;i<6;i++)
        {
            if(matsRequired[i]>0)
            {
                GameObject go = Instantiate(reqMat, reqMatContent);
                go.GetComponent<Image>().sprite = matsSprites[i];
                go.GetComponentInChildren<TMP_Text>().text = matsRequired[i].ToString();
            }
                
        }
    }
    void Update()
    {
        gameObject.GetComponent<Button>().interactable = CheckMats();
    }

    private bool CheckMats()
    {
        for(int i=0;i<6;i++)
        {
            int quantity = 0;
            foreach(var existingItem in InventoryManager.Instance.Items)
            {
                if(existingItem.id == i)
                {
                    quantity+=existingItem.quantity;
                }
            }
            if(quantity - matsRequired[i] < 0)
                return false;
        }
        return true;
    }
    public void RemoveMats()
    {
        if(!CheckMats()) return;
        for(int i=0;i<6;i++)
        {
            foreach(var existingItem in InventoryManager.Instance.Items)
            {
                if(existingItem.id == i)
                {
                    //for(int j=0;j<matsRequired[i];j++)
                    //   InventoryManager.Instance.Remove(existingItem);
                    existingItem.quantity-=matsRequired[i];
                }
            }
        }
    }
}
