using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies2Wave : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject enemy2;

    public TimeManager timeManager;
    private bool spawned;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timeManager.isNight && !spawned)
        {
            SpawnWave(timeManager.currentDay);
            spawned = true;
        }
        else if(!timeManager.isNight) spawned = false;
    }
    private void SpawnWave(int amount)
    {
        amount = Mathf.Min(amount, 6);
        for(int i=0;i<Mathf.Pow(2, amount);i++)
        {
            int index = Random.Range(0,3);
            Vector3 offset = new Vector3(Random.Range(0,2f), Random.Range(0,1f), Random.Range(0,2f));
            Instantiate(enemy2, spawnPoints[index].position + offset, transform.rotation);
        }
    }
}
