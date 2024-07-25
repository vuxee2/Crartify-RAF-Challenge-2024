using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lantern : MonoBehaviour
{
    private bool isOn;

    public Image image;
    void Update()
    {
        if(gameObject.activeSelf && !isOn)
        {
            StartCoroutine(Off());
            isOn = true;
            image.fillAmount = 1f;
        }
            
    }
    private IEnumerator Off()
    {
        
        for(int i=0;i<20;i++)
        {
            yield return new WaitForSeconds(1f);
            image.fillAmount -= .05f;
        }
        gameObject.SetActive(false);
        isOn = false;
    }
}
