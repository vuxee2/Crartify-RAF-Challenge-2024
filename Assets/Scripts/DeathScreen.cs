using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DeathScreen : MonoBehaviour
{
    public GameObject deathCanvas;

    public SpaceShip spaceship;

    public GameObject playerDeathText;
    public GameObject spaceshipDestroyedText;

    private bool isEndedGame;
    // Start is called before the first frame update
    void Start()
    {  
        isEndedGame = false;

        deathCanvas.SetActive(false);
        playerDeathText.SetActive(false);
        spaceshipDestroyedText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if((Player.health <= 0f || spaceship.health <= 0f) && !isEndedGame)
        {
            if(Player.health <= 0f) playerDeathText.SetActive(true);
            else spaceshipDestroyedText.SetActive(true);

            deathCanvas.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void returnToMainMenu()
    {
        isEndedGame = true;
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
