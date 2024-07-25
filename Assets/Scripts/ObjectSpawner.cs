using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject ObjectsToSpawnParent;
    public GameObject[] ObjectsToSpawn;

    private int ObjectsCount = 0;
    private bool ObjectsAreSpawning = false;

    public static int totalObjs;
    // Start is called before the first frame update
    void Start()
    {
       UpdateObjectsToSpawn();
       totalObjs = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(ObjectsCount>0 && !ObjectsAreSpawning && totalObjs <= 30)
        {
            StartCoroutine(Spawn(2f, Random.Range(0,ObjectsCount)));
            ObjectsAreSpawning = true;
        }
           
    }

    public void UpdateObjectsToSpawn()
    {
        ObjectsCount = 0;
        foreach (Transform child in ObjectsToSpawnParent.transform)
        {
            ObjectsCount++;
        }
        ObjectsToSpawn = new GameObject[ObjectsCount];

        int j=0;
        foreach (Transform child in ObjectsToSpawnParent.transform)
        {
            ObjectsToSpawn[j] = child.gameObject;
            j++;
        }
    }

    IEnumerator Spawn(float waitTime, int index)
    {
        yield return new WaitForSeconds(waitTime);
        totalObjs ++;

        Vector3[] position = new Vector3[3];
        position[0] = new Vector3(Random.Range(-60f, 10f), 10, Random.Range(10f, 75f));
        position[1] = new Vector3(Random.Range(18f, 57f), 10, Random.Range(63f, 77f));
        position[2] = new Vector3(Random.Range(20f, 60f), 10, Random.Range(4f, 18f));

        Vector3 desiredPos = position[Random.Range(0,3)];
        desiredPos.y = Terrain.activeTerrain.SampleHeight(desiredPos) - 11.5f;
        GameObject go = Instantiate(ObjectsToSpawn[index], desiredPos, Quaternion.identity);
        go.SetActive(true);

        go.GetComponent<ObjectStats>().SetParameters(ObjectsToSpawn[index].GetComponent<ObjectStats>().colorStats);
        
        go.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

        if(totalObjs <= 30)
            StartCoroutine(Spawn(2f, Random.Range(0,ObjectsCount)));
        else
            ObjectsAreSpawning = false;
    }

}
