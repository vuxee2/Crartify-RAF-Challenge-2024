using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float groundDist;

    public LayerMask terrainLayer;
    private Rigidbody rb;
    public SpriteRenderer sr;

    public Animator anim;

    private float staminaRenewDelay;

    public AudioSource audioSrc;
    public GameObject footsteps;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift) && Player.stamina > 0)
        {
            speed = 2;
            anim.SetFloat("animMultipl", 2);

            footsteps.GetComponent<AudioSource>().pitch = 2f;
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift) || Player.stamina <= 0) 
        {
            speed = 1;
            anim.SetFloat("animMultipl", 1);

            footsteps.GetComponent<AudioSource>().pitch = 1.3f;
        }
        
        if(Input.GetKey(KeyCode.LeftShift) && Player.stamina > 0 && (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0 ))
        {
            Player.UpdateStamina(Player.stamina - Time.deltaTime / 10);
            staminaRenewDelay = Time.time;
        }

        if(Input.GetKeyDown(KeyCode.Space) && Player.stamina >= 0.2f) 
        {
            StartCoroutine(Dodge());
            Player.UpdateStamina(Player.stamina - 0.2f);
            staminaRenewDelay = Time.time;
        }

        if(Time.time - 5 >= staminaRenewDelay && Player.stamina < 1f && Player.hunger > 0f) 
        {
            Player.UpdateStamina(Player.stamina + Time.deltaTime);
        }
    }

    IEnumerator Dodge() //dash
    {
        SoundManager.PlaySound("playerDash", audioSrc);

        speed += 4;
        anim.SetBool("isDodging", true);
        yield return new WaitForSeconds(0.2f);

        if(speed-4 > 1) speed -= 4; // da ne ide u nazad
        else speed = 1;
        anim.SetBool("isDodging", false);
    }
    void FixedUpdate()
    {
        RaycastHit hit;
        Vector3 castPos = transform.position;
        castPos.y += 1;
        if(Physics.Raycast(castPos, -transform.up, out hit, Mathf.Infinity, terrainLayer))
        {
            if(hit.collider != null)
            {
                Vector3 movePos = transform.position;
                movePos.y = hit.point.y + groundDist;
                transform.position = movePos;
            }
        }

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector3 moveDir = new Vector3(x, 0, y);
        rb.velocity = moveDir * speed;

        if(x != 0) anim.SetBool("isWalking", true);
        else anim.SetBool("isWalking", false);

        if(y > 0) anim.SetBool("isWalkingUp", true);
        else anim.SetBool("isWalkingUp", false);

        if(y < 0) anim.SetBool("isWalkingDown", true);
        else anim.SetBool("isWalkingDown", false);
        

        if(x != 0 && x < 0)
        {
            sr.flipX = false;
        }
        else if(x != 0 && x > 0)
        {
            sr.flipX = true;
        }

        if(rb.velocity != Vector3.zero)
        {
            footsteps.SetActive(true);
        }
        else footsteps.SetActive(false);
    }
}
