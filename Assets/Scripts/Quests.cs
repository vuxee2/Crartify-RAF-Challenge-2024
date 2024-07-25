using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class Quests : MonoBehaviour
{
    public GameObject quest1;
    public GameObject quest2;
    public GameObject questNight;

    public TimeManager timeManager;

    public Image quest2CompleteBar;
    public TMP_Text quest2CompleteTXT;
    private int quest2CompleteNumber;
    public GameObject enemyAreasParent;

    public GameObject fadeIn;
    private bool endedGame;
    // Start is called before the first frame update
    void Start()
    {
        endedGame = false;

        setActiveQuest(quest1);
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.FindGameObjectWithTag("paintStation") != null)
        {
            setActiveQuest(quest2);
        }
        if(timeManager.isNight && !quest1.activeSelf)
        {
            setActiveQuest(questNight);
        }

        quest2CompleteNumber = enemyAreasParent.transform.childCount;
        quest2CompleteBar.fillAmount = (10f - (float)quest2CompleteNumber) / 10;
        quest2CompleteTXT.text = (10 - quest2CompleteNumber).ToString() + " / 10";

        if(quest2CompleteNumber == 0 && !endedGame)
        {
            endedGame = true;
            StartCoroutine(endGame());
        }
    }

    private void setActiveQuest(GameObject quest)
    {
        quest1.SetActive(false);
        quest2.SetActive(false);
        questNight.SetActive(false);

        quest.SetActive(true);
    }

    private IEnumerator endGame()
    {
        Instantiate(fadeIn, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("endStory");
    }
}
