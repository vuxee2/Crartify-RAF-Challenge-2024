using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LineGenerator : MonoBehaviour
{
    public GameObject[] linePrefabs; //0-brown, 1-green, 2-red, 3-dark red, 4-grey, 5-blue
    private int colorIndex = 0;
    public GameObject ChangeColorButton;

    public GameObject LineParent;

    Line activeLine;

    public Camera paintCam;

    private bool wasMBDown = false; //fix za paintstaion kad se postavi

    public bool isDrawingTree = true;
    public GameObject bgTree;
    public GameObject bgStone;

    public TMP_Text statsText;

    void Start()
    {
        bgTree.SetActive(true);
        bgStone.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            GetStats();
        }

        if(Input.GetMouseButtonDown(0))
        {
            GameObject newLine = Instantiate(linePrefabs[colorIndex], LineParent.transform.position, Quaternion.identity);
            activeLine = newLine.GetComponent<Line>();

            newLine.transform.parent = LineParent.transform;
            wasMBDown = true;
        }

        if(Input.GetMouseButtonUp(0) && wasMBDown && activeLine != null)
        {
            if(activeLine.gameObject.GetComponent<LineRenderer>().positionCount < 2) 
                Destroy(activeLine.gameObject);
            activeLine = null;

            wasMBDown = false;
        }
        if(Input.GetMouseButton(0) && Input.GetMouseButtonDown(1))
        {
            int totalLines = LineParent.transform.childCount;
            if(totalLines > 1)
                Destroy(LineParent.transform.GetChild(totalLines-1).gameObject);
            activeLine = null;
        }

        if(activeLine != null)
        {
            Vector2 mousePos = paintCam.ScreenToWorldPoint(Input.mousePosition);

            mousePos.x = Mathf.Clamp(mousePos.x, -4.9f, 4.9f);
            mousePos.y = Mathf.Clamp(mousePos.y, -4.9f, 4.9f);

            activeLine.UpdateLine(mousePos);
        }
    }
    public void ChangeColor()
    {
        colorIndex++;

        if(isDrawingTree)
            colorIndex%=linePrefabs.Length/2;
        else
            colorIndex = colorIndex % (linePrefabs.Length/2) + (linePrefabs.Length/2);

        ChangeColorButton.GetComponent<Image>().material = linePrefabs[colorIndex].GetComponent<Renderer>().sharedMaterial;
    }

    public void Undo()
    {
        int totalLines = LineParent.transform.childCount;
        if(totalLines > 2)
            Destroy(LineParent.transform.GetChild(totalLines-2).gameObject);
    }

    public void Delete()
    {
        int totalLines = LineParent.transform.childCount;
        for(int i=1;i<totalLines;i++)
            Destroy(LineParent.transform.GetChild(i).gameObject);
    }

    public void ChangeMode()
    {
        Delete();
        isDrawingTree = !isDrawingTree;
        bgTree.SetActive(isDrawingTree);
        bgStone.SetActive(!isDrawingTree);

        ChangeColor();
    }

    public int GetStats()
    {
        int[] stats;
        stats = new int[6];

        int health;
        health = 0;

        for (var i = LineParent.transform.childCount - 1; i >= 0; i--)
            {
                if(LineParent.transform.GetChild(i).GetComponent<LineRenderer>() != null)
                {
                    switch(LineParent.transform.GetChild(i).GetComponent<LineRenderer>().material.name)
                    {
                        case "InkBrown (Instance)":
                        {
                            stats[0]+=LineParent.transform.GetChild(i).GetComponent<LineRenderer>().positionCount;;
                            break;
                        }
                        case "InkGreen (Instance)":
                        {
                            stats[1]+=LineParent.transform.GetChild(i).GetComponent<LineRenderer>().positionCount;;
                            break;
                        }
                        case "InkRed (Instance)":
                        {
                            stats[2]+=LineParent.transform.GetChild(i).GetComponent<LineRenderer>().positionCount;;
                            break;
                        }
                        case "InkDarkRed (Instance)":
                        {
                            stats[3]+=LineParent.transform.GetChild(i).GetComponent<LineRenderer>().positionCount;;
                            break;
                        }
                        case "InkGrey (Instance)":
                        {
                            stats[4]+=LineParent.transform.GetChild(i).GetComponent<LineRenderer>().positionCount;;
                            break;
                        }
                        case "InkBlue (Instance)":
                        {
                            stats[5]+=LineParent.transform.GetChild(i).GetComponent<LineRenderer>().positionCount;;
                            break;
                        }
                    }
                    health+=LineParent.transform.GetChild(i).GetComponent<LineRenderer>().positionCount;
                }
            }
        /*Debug.Log("Woods: " + stats[0]/50);
        Debug.Log("Sticks: " + stats[1]/50);
        Debug.Log("Apples: " + stats[2]/50);
        Debug.Log("Ruby: " + stats[3]/50);
        Debug.Log("Stone: " + stats[4]/50);
        Debug.Log("Sapphire: " + stats[5]/50);
        Debug.Log("Health: " + health);*/

        statsText.text = ("Wood: " + (stats[0]/50).ToString()) + 
                        ("<br>Stick: " + (stats[1]/50).ToString()) + 
                        ("<br>Apple: " + (stats[2]/50).ToString()) + 
                        ("<br>Ruby: " + (stats[3]/50).ToString()) + 
                        ("<br>Stone: " + (stats[4]/50).ToString()) + 
                        ("<br>Sapphire: " + (stats[5]/50).ToString()) + 
                        ("<br><br>Health: " + health.ToString());

        return health;
    }
}
