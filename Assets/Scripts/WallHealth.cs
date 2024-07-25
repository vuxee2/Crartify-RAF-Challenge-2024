using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class WallHealth : MonoBehaviour
{
    public GameObject parent;
    public GameObject Canvas;

    public float health;
    public float maxHealth;

    public TMP_Text healthTXT;
    public Image healthBar;
    
    void Start()
    {
        Canvas.SetActive(false);

        health = maxHealth;
    }
    void Update()
    {
       /* Canvas.transform.eulerAngles = new Vector3(
        Canvas.transform.eulerAngles.x,
        Canvas.transform.eulerAngles.y + Time.deltaTime * 100,
        Canvas.transform.eulerAngles.z
        );*/

        healthTXT.text = health.ToString();
        healthBar.fillAmount = health / maxHealth;
        if(health<=0) 
        {
            Destroy(parent);
        }
    }
    public void UpdateHealth(float newHealth)
    {
        health = newHealth;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player")
        {
            Canvas.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "Player")
        {
            Canvas.SetActive(false);
        }
    }
}
