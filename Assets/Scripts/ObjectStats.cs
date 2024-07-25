using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ObjectStats : MonoBehaviour
{
    public int[] colorStats;
    public GameObject[] treeMatsPrefs;
    public GameObject[] stoneMatsPrefs;

    public int health;

    public TMP_Text healthTXT;

    public bool isTree;

    public GameObject particle;
    void Awake()
    {
        colorStats = new int[10];
        healthTXT.text = health.ToString();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetParameters(int[] a)
    {
        for(int i=0;i<a.Length;i++)
        {
            colorStats[i] = a[i];  
        }
        
    }


    public void ResetStats()
    {
        colorStats = new int[10];
        health = 0;
    }

    void OnTriggerEnter(Collider coll)
    {
        if((coll.tag == "axe" && isTree) || (coll.tag == "pickaxe" && !isTree))
        {
            Instantiate(particle, coll.gameObject.transform.position, Quaternion.identity);

            health-=25;
            healthTXT.text = health.ToString();

            SoundManager.defPlaySound("objChop");

            if(health <= 0){
                DropMats();
                ObjectSpawner.totalObjs --;
                Destroy(gameObject);
            }
        }
    }

    private void DropMats()
    {
        if(isTree)
        {
            for(int i=0;i<3;i++)
            {
                for(int j=0;j<colorStats[i]/50;j++)
                {
                    Vector3 rndOffset = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(0f, 0.5f), Random.Range(-0.5f, 0.5f));
                    Instantiate(treeMatsPrefs[i], transform.position + rndOffset, Quaternion.identity);
                }
            }
        }
        else
        {
            for(int i=0;i<3;i++)
            {
                for(int j=0;j<colorStats[i+3]/50;j++)
                {
                    Vector3 rndOffset = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(0f, 0.5f), Random.Range(-0.5f, 0.5f));
                    Instantiate(stoneMatsPrefs[i], transform.position + rndOffset, Quaternion.identity);
                }
            }
        }
    }
}
