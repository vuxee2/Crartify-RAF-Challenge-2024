using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy2 : MonoBehaviour
{
    public int health = 100;
    public float speed = 0.5f;

    private GameObject spaceShip;

    public Slider healthSlider;

    private bool shouldReduceHealthDefensePlate = false;

    public SpriteRenderer sr;

    public AudioSource audioSrc;

    public GameObject destroyParticle;
    public GameObject attackParticle;
    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreLayerCollision(10, 10);

        spaceShip = GameObject.FindWithTag("spaceship");
        FixOrientation(0);

        healthSlider.gameObject.SetActive(false);
        healthSlider.value = health;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void FixOrientation(int i)
    {
        Vector3 target = spaceShip.transform.position;
        if(i==0)
            if(target.x < transform.position.x) sr.flipX = true;
            else sr.flipX = false;
        else if(i==180)
            if(target.x > transform.position.x) sr.flipX = true;
            else sr.flipX = false;
    }

    public void Move()
    {
        Vector3 target = spaceShip.transform.position;
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    public void OnCollisionEnter(Collision coll)
    {
        if(coll.gameObject.tag == "spaceship")
        {
            SoundManager.PlaySound("enemy2Attack", audioSrc);

            coll.gameObject.GetComponent<SpaceShip>().UpdateHealth(coll.gameObject.GetComponent<SpaceShip>().health - 1);
            StartCoroutine("AttackWithDelay");
        }
        if(coll.gameObject.tag == "defensePlate")
        {
            shouldReduceHealthDefensePlate = true;
            healthSlider.gameObject.SetActive(true);
            StartCoroutine(reduceHealthDefensePlate());
        }
        if(coll.gameObject.tag == "wall")
        {
            SoundManager.PlaySound("enemy2Attack", audioSrc);

            coll.gameObject.GetComponent<WallHealth>().UpdateHealth(coll.gameObject.GetComponent<WallHealth>().health - 5f);
            StartCoroutine("AttackWithDelay");
        }
    }

    private IEnumerator AttackWithDelay()
    {
        speed*=-1;
        FixOrientation(180);
        yield return new WaitForSeconds(1);
        speed*=-1;
        FixOrientation(0);
    }
    private void OnCollisionExit(Collision coll)
    {
        if(coll.gameObject.tag == "defensePlate")
        {
            shouldReduceHealthDefensePlate = false;
        }
    }
    private IEnumerator reduceHealthDefensePlate()
    {
        yield return new WaitForSeconds(0.5f);
        SoundManager.PlaySound("enemy2Damage", audioSrc);
        HealthReduce(5);
        if(shouldReduceHealthDefensePlate)StartCoroutine(reduceHealthDefensePlate());
    }
    private void OnTriggerEnter(Collider coll)
    {
        if(coll.tag == "sword")
        {
            Instantiate(attackParticle, transform.position, Quaternion.identity);
            SoundManager.PlaySound("enemy2Damage", audioSrc);
            healthSlider.gameObject.SetActive(true);
            HealthReduce(20);
        }
    }
    private void OnTriggerStay(Collider coll)
    {
        if(coll.tag == "shootingTower")
        {
            if(coll.gameObject.GetComponent<ShootingTower>().focusedEnemy == null && coll.gameObject.GetComponentInChildren<BoxCollider>().enabled)
            {
                healthSlider.gameObject.SetActive(true);
                coll.gameObject.GetComponent<ShootingTower>().SetFocusedEnemy(gameObject);
            }
        }
    }
    private void OnTriggerExit(Collider coll)
    {
        if(coll.tag == "shootingTower")
        {
            healthSlider.gameObject.SetActive(false);
            coll.gameObject.GetComponent<ShootingTower>().SetFocusedEnemy(null);
        }
    }
    public void HealthReduce(int amount)
    {
        health -= amount;
        UpdateHealthSlider();
        if(health<=0) 
        {
            Instantiate(destroyParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void UpdateHealthSlider()
    {
        healthSlider.value = health;
    }
}
