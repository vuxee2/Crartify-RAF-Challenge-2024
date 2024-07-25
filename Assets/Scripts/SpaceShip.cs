using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpaceShip : MonoBehaviour
{
    public GameObject Canvas;
    public GameObject buildUI;
    private bool isPlayerNear;

    float lerpSpeed;
    public int health;
    private float maxHealth = 100;
    public TMP_Text healthTXT;
    public Image healthBar;   
    // Start is called before the first frame update
    void Start()
    {
        isPlayerNear = false;

        lerpSpeed = 3f * Time.deltaTime;
        
        health = (int)maxHealth;
        healthTXT.text = health.ToString();

        Canvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        healthTXT.text = health.ToString(); 
        healthBar.fillAmount =  Mathf.Lerp(healthBar.fillAmount, (health / maxHealth), lerpSpeed);
        Color healthColor = Color.Lerp(Color.red, Color.green, (health / maxHealth));
        healthBar.color = healthColor;

        if(Input.GetKeyDown("e") && isPlayerNear)
        {
            buildUI.SetActive(!buildUI.activeSelf);
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            buildUI.SetActive(false);
        }
    }
    public void UpdateHealth(int newHealth)
    {
        health = newHealth;
    }

    private void OnTriggerEnter(Collider coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            isPlayerNear = true;

            Canvas.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            isPlayerNear = false;

            Canvas.SetActive(false);
            buildUI.SetActive(false);
        }
    }
}
