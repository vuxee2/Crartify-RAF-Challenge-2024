using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintStation : MonoBehaviour
{
    [SerializeField]
    private GameObject UI, mainCamera, paintCamera, paintingParent;

    public Collider objColl;

    public GameObject canvas;
    void Awake()
    {
        paintingParent = GameObject.Find("PaintingParent");
        mainCamera = GameObject.Find("Main Camera");

        Transform[] trs = paintingParent.GetComponentsInChildren<Transform>(true);
        foreach(Transform t in trs){
            if(t.name == "Painting"){
                UI = t.gameObject;
            }
            if(t.name == "PaintCamera"){
                paintCamera = t.gameObject;
            }
        }
    }
    private void OnTriggerEnter(Collider coll)
    {
        if(coll.gameObject.tag == "Player" && objColl.enabled)
        {
            canvas.SetActive(true);
        }
    }
    private void OnTriggerStay(Collider coll)
    {
        if(coll.gameObject.tag == "Player" && objColl.enabled && Input.GetKey("e"))
        {
            UI.SetActive(true);
            mainCamera.SetActive(false);
            paintCamera.SetActive(true);
        }
        if(Input.GetKey(KeyCode.Escape))
        {
            UI.SetActive(false);
            paintCamera.SetActive(false);
            mainCamera.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider coll )
    {
        if(coll.gameObject.tag == "Player" && objColl.enabled)
        {
            UI.SetActive(false);
            paintCamera.SetActive(false);
            mainCamera.SetActive(true);

            canvas.SetActive(false);
        }
    }
}
