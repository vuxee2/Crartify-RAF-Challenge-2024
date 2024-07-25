using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startStory : MonoBehaviour
{
    public GameObject fadeOut;
    public GameObject fadeIn;
    void Awake()
    {
        Instantiate(fadeOut, transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(StartGame());
        }
    }

    private IEnumerator StartGame()
    {
        Instantiate(fadeIn, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1f);

        if(SceneManager.GetActiveScene().name == "startStory")
            SceneManager.LoadScene("SampleScene");
        else
            SceneManager.LoadScene("MainMenu");
    }
}
