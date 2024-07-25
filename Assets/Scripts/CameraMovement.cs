using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Vector3 offset = new Vector3 (0f, 1f, -2.5f);
    private float smoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;

    public Transform target;

    void Start()
    {
        //this.GetComponent<Camera>().fieldOfView = 60.0f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }

    /*private void zoomCamera()
    {
        if(Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            if(Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                this.GetComponent<Camera>().fieldOfView += 5;
            }
            else if(Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                this.GetComponent<Camera>().fieldOfView -= 5;
            }

            this.GetComponent<Camera>().fieldOfView = Mathf.Clamp(this.GetComponent<Camera>().fieldOfView, 60, 110);
        }
    }*/
}
