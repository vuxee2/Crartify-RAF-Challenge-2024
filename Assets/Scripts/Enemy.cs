using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float speed = 3f;
    public float jumpForce = 6f;
    public float xPos;
    public float zPos;
    public Vector3 desiredPos;

    public Animator anim;
    private Rigidbody rb;

    private bool isInAir = false;

    private int health;
    public Slider healthSlider;

    private GameObject player;
    private bool shouldLookForAttack = false;
    public SpriteRenderer enemySprite;

    public GameObject Area;
    private float areaRadius;

    public GameObject targetPrefab;

    public GameObject stickDrop;

    public AudioSource audioSrc;

    public GameObject destroyParticle;
    public GameObject attackParticle;
    // Start is called before the first frame update
    void Start()
    {
        areaRadius = Area.GetComponent<SphereCollider>().radius; 

        player = GameObject.FindGameObjectWithTag("Player");

        healthSlider.gameObject.SetActive(false);
        health = 100;
        UpdateHealthSlider();

        StartCoroutine(Move());
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < - 10) Destroy(gameObject);
        if(isInAir)
            transform.position = Vector3.Lerp(transform.position, desiredPos, Time.deltaTime * speed);

        if (Vector3.Distance(transform.position, desiredPos) <= 0.2f && isInAir) 
        {
            anim.SetBool("jumped",false);
            anim.SetBool("done", true);
            isInAir = false;
            if(shouldLookForAttack)
                StartCoroutine(Attack());
            StartCoroutine(Move());
        }
        if(shouldLookForAttack)
        {
            if((transform.position - Area.transform.position).magnitude > 2 * areaRadius) 
            {
                shouldLookForAttack = false;
                enemySprite.color = new Color(1f, 1f, 1f, 1f);
                anim.SetBool("attack", false);
            }
        }

        if(transform.parent.gameObject.GetComponent<EnemiesArea>().isAttacked)
        {
            enemySprite.color = new Color(1f, 0.5f, 0.5f, 1f);
            shouldLookForAttack = true;
            healthSlider.gameObject.SetActive(true);
        }
        else
        {
            enemySprite.color = new Color(1f, 1f, 1f, 1f);
            shouldLookForAttack = false;
            healthSlider.gameObject.SetActive(false);
        }
    }

    private IEnumerator Move()
    {
        yield return new WaitForSeconds(2f);

        anim.SetBool("jump", true);
        yield return new WaitForSeconds(1f);
        anim.SetBool("jump", false);
        rb.AddForce(gameObject.transform.up * jumpForce, ForceMode.Impulse);

        SoundManager.PlaySound("enemyJump", audioSrc);

        anim.SetBool("done", false);
        anim.SetBool("jumped",true);

        if(!shouldLookForAttack)
        {
            xPos = Area.transform.position.x + Random.Range(areaRadius, areaRadius *-1);
            zPos = Area.transform.position.z + Random.Range(areaRadius, areaRadius *-1);
        }
        else
        {
            xPos = player.transform.position.x;
            zPos = player.transform.position.z;

            Instantiate(targetPrefab, player.transform.position , Quaternion.identity);
        }

        
        desiredPos = new Vector3(xPos, transform.position.y, zPos);
        desiredPos.y = Terrain.activeTerrain.SampleHeight(desiredPos) - 12.5f; //ovo 12.5f je zato sto terrain.y nije na 0, nego na 12.9 otp
        isInAir = true;
    }

    private void UpdateHealthSlider()
    {
        healthSlider.value = health;
    }

    private void OnTriggerEnter(Collider coll)
    {
        if(coll.tag == "sword")
        {
            Instantiate(attackParticle, transform.position, Quaternion.identity);

            SoundManager.PlaySound("enemyDamage", audioSrc);

            transform.parent.gameObject.GetComponent<EnemiesArea>().isAttacked = true;
            enemySprite.color = new Color(1f, 0.5f, 0.5f, 1f);
            shouldLookForAttack = true;
            healthSlider.gameObject.SetActive(true);
            health -= 10;
            UpdateHealthSlider();
            
            if(health<=0) 
            {
                Instantiate(destroyParticle, transform.position, Quaternion.identity);
                Instantiate(stickDrop, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }

    private IEnumerator Attack()
    {
        anim.SetBool("attack", true);
        yield return new WaitForSeconds(0.5f);

        SoundManager.PlaySound("enemyBite", audioSrc);

        if((transform.position - player.transform.position).magnitude < 1)
        {
            Player.UpdateHealth(Player.health - 5);
        }

        anim.SetBool("attack", false);
        yield return new WaitForSeconds(0.5f);
    }

}
