using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public Collider objColl;
    private void OnTriggerEnter(Collider coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            objColl.enabled = false;
        }
    }
    private void OnTriggerExit(Collider coll )
    {
        if(coll.gameObject.tag == "Player")
        {
            objColl.enabled = true;
        }
    }
}
