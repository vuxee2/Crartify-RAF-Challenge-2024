using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip enemyJump, enemyBite, enemyDamage;
    public static AudioClip playerDash, playerDamage, playerEat;
    public static AudioClip enemy2Attack, enemy2Damage;
    public static AudioClip itemPickup, objChop;

    public static AudioSource defAudioSrc;

    public TimeManager timeManager;
    public Animator dayNightAnim;
    // Start is called before the first frame update
    void Start()
    {
        defAudioSrc = GetComponent<AudioSource>();

        enemyJump = Resources.Load<AudioClip>("enemyJump");
        enemyBite = Resources.Load<AudioClip>("enemyBite");
        enemyDamage = Resources.Load<AudioClip>("enemyDamage");

        playerDash = Resources.Load<AudioClip>("playerDash");
        playerDamage = Resources.Load<AudioClip>("playerDamage");
        playerEat = Resources.Load<AudioClip>("playerEat");

        enemy2Attack = Resources.Load<AudioClip>("enemy2Attack");
        enemy2Damage = Resources.Load<AudioClip>("enemy2Damage");

        itemPickup = Resources.Load<AudioClip>("itemPickup");
        objChop = Resources.Load<AudioClip>("objChop");

    }

    void Update()
    {
        dayNightAnim.SetBool("isNight", timeManager.isNight);
    }

    public static void PlaySound(string clip, AudioSource audioSrc)
    {
        switch(clip)
        {
            case "enemyJump":
                audioSrc.PlayOneShot(enemyJump);
                break;
            case "enemyBite":
                audioSrc.PlayOneShot(enemyBite);
                break;
            case "enemyDamage":
                audioSrc.PlayOneShot(enemyDamage);
                break;
            case "playerDash":
                audioSrc.PlayOneShot(playerDash);
                break;
            case "playerDamage":
                audioSrc.PlayOneShot(playerDamage);
                break;
            case "enemy2Attack":
                audioSrc.PlayOneShot(enemy2Attack);
                break;
            case "enemy2Damage":
                audioSrc.PlayOneShot(enemy2Damage);
                break;
        }
    }
    public static void defPlaySound(string clip)
    {
        switch(clip)
        {
            case "itemPickup":
                defAudioSrc.PlayOneShot(itemPickup);
                break;
            case "playerEat":
                defAudioSrc.PlayOneShot(playerEat);
                break;
            case "objChop":
                defAudioSrc.PlayOneShot(objChop);
                break;
        }
    }
            
}
