using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject optionsPanel;

    public GameObject fadeOut;
    public GameObject fadeIn;
    void Awake()
    {
        Instantiate(fadeOut, transform.position, Quaternion.identity);
    }
    public void Play()
    {
        StartCoroutine(StartStory());
    }
    public void Options()
    {
        optionsPanel.SetActive(!optionsPanel.activeSelf);
    }
    public void Exit()
    {
        Application.Quit();
    }

    private IEnumerator StartStory()
    {
        Instantiate(fadeIn, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("startStory");
    }
}
