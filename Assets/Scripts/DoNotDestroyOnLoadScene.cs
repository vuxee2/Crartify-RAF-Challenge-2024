using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoNotDestroyOnLoadScene : MonoBehaviour
{
    private void Awake() 
    {   
        DontDestroyOnLoad(gameObject);
    }
    private void Update() {
        if(SceneManager.GetActiveScene().name == "SampleScene") Destroy(gameObject);
    }
}
