using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineConfirming : MonoBehaviour
{
    public GameObject LineParent;
    public GameObject ObjectsToSpawn;
    public GameObject Spawner;
    public LineGenerator lineGenerator;
    
    public void ConfirmLine()
    {
        LineParent.GetComponent<ObjectStats>().isTree = lineGenerator.isDrawingTree;
        if(LineParent.transform.childCount > 1 && lineGenerator.GetStats() > 10)
        {
            for (var i = LineParent.transform.childCount - 1; i >= 0; i--) //ovo
            {
                if(LineParent.transform.GetChild(i).GetComponent<LineRenderer>() != null)
                {
                    switch(LineParent.transform.GetChild(i).GetComponent<LineRenderer>().material.name)
                    {
                        case "InkBrown (Instance)":
                        {
                            LineParent.GetComponent<ObjectStats>().colorStats[0]+=LineParent.transform.GetChild(i).GetComponent<LineRenderer>().positionCount;
                            break;
                        }
                        case "InkGreen (Instance)":
                        {
                            LineParent.GetComponent<ObjectStats>().colorStats[1]+=LineParent.transform.GetChild(i).GetComponent<LineRenderer>().positionCount;
                            break;
                        }
                        case "InkRed (Instance)":
                        {
                            LineParent.GetComponent<ObjectStats>().colorStats[2]+=LineParent.transform.GetChild(i).GetComponent<LineRenderer>().positionCount;
                            break;
                        }
                        case "InkDarkRed (Instance)":
                        {
                            LineParent.GetComponent<ObjectStats>().colorStats[3]+=LineParent.transform.GetChild(i).GetComponent<LineRenderer>().positionCount;
                            break;
                        }
                        case "InkGrey (Instance)":
                        {
                            LineParent.GetComponent<ObjectStats>().colorStats[4]+=LineParent.transform.GetChild(i).GetComponent<LineRenderer>().positionCount;
                            break;
                        }
                        case "InkBlue (Instance)":
                        {
                            LineParent.GetComponent<ObjectStats>().colorStats[5]+=LineParent.transform.GetChild(i).GetComponent<LineRenderer>().positionCount;
                            break;
                        }
                    }
                    LineParent.GetComponent<ObjectStats>().health+=LineParent.transform.GetChild(i).GetComponent<LineRenderer>().positionCount;
                }
            }

            GameObject go = GameObject.Instantiate(LineParent);
            go.transform.position = LineParent.transform.position;
            go.transform.parent = ObjectsToSpawn.transform;

            go.GetComponent<ObjectStats>().SetParameters(LineParent.GetComponent<ObjectStats>().colorStats);
    
            Rigidbody rb = go.AddComponent<Rigidbody>();
            //rb.freezeRotation = true;
            rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation;

            CapsuleCollider cc = go.AddComponent(typeof(CapsuleCollider)) as CapsuleCollider;
            cc.radius = 1f;
            cc.height = 7f;

            go.SetActive(false);

            for (var i = LineParent.transform.childCount - 1; i >= 0; i--)
            {
                if(LineParent.transform.GetChild(i).GetComponent<LineRenderer>() != null)
                    Destroy(LineParent.transform.GetChild(i).gameObject);
            }
        }

        Spawner.GetComponent<ObjectSpawner>().UpdateObjectsToSpawn();
        LineParent.GetComponent<ObjectStats>().ResetStats();
    }
}
