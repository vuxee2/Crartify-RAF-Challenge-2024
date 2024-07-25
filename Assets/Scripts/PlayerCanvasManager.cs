using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCanvasManager : MonoBehaviour
{
    public GameObject InventoryCanvas;
    public Animator InventoryCanvasAnim;

    public GameObject pauseCanvas;
    // Start is called before the first frame update
    void Start()
    {
        InventoryCanvas.SetActive(false);
        pauseCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(invCanvas());

        if(Input.GetKeyDown("p"))
        {
            pauseCanvas.SetActive(!pauseCanvas.activeSelf);
            Time.timeScale = pauseCanvas.activeSelf? 0f : 1f;
        }
        if(Input.GetKeyDown(KeyCode.Escape) && pauseCanvas.activeSelf)
        {
            pauseCanvas.SetActive(false);
             Time.timeScale = 1f;
        }
    }
    private IEnumerator invCanvas()
    {
        if(Input.GetKeyDown(KeyCode.Tab) && !InventoryCanvas.activeSelf)
        {
            InventoryCanvas.SetActive(true);
            InventoryManager.Instance.ListItems();

            yield return new WaitForSeconds(0.1f);
            InventoryCanvasAnim.SetBool("open", true);
        }
        if(Input.GetKeyDown(KeyCode.Escape) || (Input.GetKeyDown(KeyCode.Tab) && InventoryCanvas.activeSelf))
        {
            InventoryCanvasAnim.SetBool("open", false);
            yield return new WaitForSeconds(0.1f);

            InventoryCanvas.SetActive(false);
            InventoryManager.Instance.CleanList();
        }
    }
    public void MainMenuButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
