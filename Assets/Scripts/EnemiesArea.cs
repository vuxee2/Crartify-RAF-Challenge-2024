using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemiesArea : MonoBehaviour
{
    public int enemyCount = 5;
    public bool isAttacked;
    private bool wasAttacked;
    private GameObject player;

    private float radius;

    public GameObject enemyPrefab;
    
    public int spawnRate;

    private bool trebaMiJerSamGlup;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemies());

        radius = GetComponent<SphereCollider>().radius;
        if(SceneManager.GetActiveScene().name != "MainMenu") player = GameObject.FindGameObjectWithTag("Player"); // za mainmenu
        isAttacked = false;
        wasAttacked = false;

        trebaMiJerSamGlup = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "MainMenu") return; //za mainmenu

        if(isAttacked)
        {
            if((transform.position - player.transform.position).magnitude > radius * 2)
            {
                isAttacked = false;
                wasAttacked = true;
            } 
        }
        if((transform.position - player.transform.position).magnitude < radius && wasAttacked)
        {
            isAttacked = true;
        }
    }

    private IEnumerator SpawnEnemies()
    {
        if(transform.childCount == 2 && trebaMiJerSamGlup)
        {
            Destroy(gameObject);
        }
        else if(transform.childCount == 2)
        {
            trebaMiJerSamGlup = true;
        }
        if(transform.childCount < enemyCount + 2)
        {
            GameObject newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            newEnemy.transform.parent = gameObject.transform;
            newEnemy.SetActive(true);
        }
        yield return new WaitForSeconds(spawnRate);
        StartCoroutine(SpawnEnemies());
    }
}
