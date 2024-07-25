using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public Item Item;

    private GameObject player;
    private bool shouldFollow;
    private Vector3 velocity = Vector3.zero;
    private float smoothTime = .2f;
    void Start()
    {
        shouldFollow = false;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Pickup()
    {
        SoundManager.defPlaySound("itemPickup");

        InventoryManager.Instance.Add(Item);
        Destroy(gameObject);
    }
    
    private void OnCollisionEnter(Collision coll)
    {
        if(coll.gameObject.tag == "Player")
            Pickup();
    }
    
    void Update()
    {
        if(shouldFollow)
        {
            Vector3 targetPosition = player.transform.position;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }

    private void OnTriggerEnter(Collider coll)
    {
        if(coll.gameObject.tag == "Player")
            shouldFollow = true;
    }
    private void OnTriggerExit(Collider coll)
    {
        if(coll.gameObject.tag == "Player")
            shouldFollow = false;
    }

}
