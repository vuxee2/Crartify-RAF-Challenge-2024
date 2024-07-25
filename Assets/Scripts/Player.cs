using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    float lerpSpeed;

    //[Header("Health")]
    public static int health;
    private float maxHealth = 100;
    public TMP_Text healthTXT;
    public Image healthBar;    

    //[Header("Stamina")]
    public static float stamina;
    private float maxStamina = 1;
    public TMP_Text staminaTXT;
    public Image staminaBar;

    //[Header("Hunger")]
    public static int hunger;
    private float maxHunger = 100;
    public TMP_Text hungerTXT;
    public Image hungerBar;

    public AudioSource audioSrc;
    public static bool shouldPlayDamageSound = false;
    void Start()
    {
        lerpSpeed = 3f * Time.deltaTime;

        health = (int)maxHealth;
        healthTXT.text = health.ToString();

        stamina = maxStamina;
        staminaTXT.text = ((int)(stamina*100)).ToString();

        hunger = (int)maxHunger;
        hungerTXT.text = hunger.ToString();
        InvokeRepeating("ReduceHunger", 3f, 3f);
    }

    void Update()
    {
        healthTXT.text = health.ToString(); 
        healthBar.fillAmount =  Mathf.Lerp(healthBar.fillAmount, (health / maxHealth), lerpSpeed);
        Color healthColor = Color.Lerp(Color.red, Color.green, (health / maxHealth));
        healthBar.color = healthColor;

        staminaTXT.text = ((int)(stamina*100)).ToString();
        staminaBar.fillAmount =  Mathf.Lerp(staminaBar.fillAmount, stamina, lerpSpeed);

        hungerTXT.text = hunger.ToString();
        hungerBar.fillAmount =  Mathf.Lerp(hungerBar.fillAmount, (hunger/maxHunger), lerpSpeed);
        CheckIfHungry();

        if(shouldPlayDamageSound)
        {
            shouldPlayDamageSound = false;
            SoundManager.PlaySound("playerDamage", audioSrc);
        }
    }

    public static void UpdateHealth(int newHealth)
    {
        if(newHealth<health) shouldPlayDamageSound = true;

        health = newHealth;
    }

    public static void UpdateStamina(float newStamina)
    {
        stamina = newStamina;
    }
    public static void UpdateHunger(int newHunger)
    {
        hunger = newHunger;
    }

    private void ReduceHunger()
    {
        hunger -= 1;
    }
    private void CheckIfHungry()
    {
        if(hunger <= 0) //ovo se treba promeni
        {
            hunger = 0;
            //health da se reduce
            //mnogo mi se spava nmg sad

            //ne treba realno
        }
    }
}
