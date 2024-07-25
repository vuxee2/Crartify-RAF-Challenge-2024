using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTower : MonoBehaviour
{
    public LineRenderer lineRend;
    public GameObject icosphere;
    private bool isShooting;
    public GameObject focusedEnemy;

    public GameObject shootingSFX;

    void Start()
    {
        isShooting = false;
        focusedEnemy = null;
    }
    void Update()
    {
        shootingSFX.SetActive(lineRend.enabled);

        if(focusedEnemy != null && gameObject.GetComponentInChildren<BoxCollider>().enabled)
        {
            Shoot();
        }
        else lineRend.enabled = false;
    }
    public void SetFocusedEnemy(GameObject en)
    {
        focusedEnemy = en;
    }
    public void Shoot()
    {
        lineRend.enabled = true;
        lineRend.SetPosition(0, icosphere.transform.position);
        lineRend.SetPosition(1, focusedEnemy.transform.position);
        
        if(!isShooting)
        {
            StartCoroutine("ShootEnemy");
            isShooting = true;
        }
    }
    public void EndShooting()
    {
        focusedEnemy = null;
        lineRend.enabled = false;
        isShooting = false;
    }

    private IEnumerator ShootEnemy()
    {
        yield return new WaitForSeconds(0.1f);
        if(focusedEnemy!=null)
        { 
            focusedEnemy.GetComponent<Enemy2>().HealthReduce(3);
            StartCoroutine("ShootEnemy");
        }

        else EndShooting();
    }
}
