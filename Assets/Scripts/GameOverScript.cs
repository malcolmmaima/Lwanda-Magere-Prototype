using UnityEngine;
using UnityEngine.UI;

// Start or quit the game

public class GameOverScript : MonoBehaviour
{
    private Button[] buttons;
    private bool paused;

    void Awake()
    {
        // Get the buttons
        buttons = GetComponentsInChildren<Button>();
        paused = false;

        HideButtons();
    }

    public void HideButtons()
    {
        foreach (var b in buttons)
        {
            b.gameObject.SetActive(false);
        }
    }

    public void ShowButtons()
    {
        foreach (var b in buttons)
        {
            b.gameObject.SetActive(true);
        }
    }

    public void ExitToMenu()
    {
        Time.timeScale = 1;
        Debug.Log("Exit to Menu");
        Application.LoadLevel("Menu");
    }

    public void Resume()
    {
        
        
        if (paused) //If game is in paused state then change pause state and hidebuttons
        {
            //paused = false;
            Debug.Log("Game paused...unpause");
            Time.timeScale = 1;
            HideButtons();
        } 
    }

    public void RestartGame()
    {
        //Make sure game is not in paused state, if in paused state unpause
        //paused = false;
        Time.timeScale = 1; 

        // Reload the level
        Debug.Log("Restart Game");
        Application.LoadLevel(Application.loadedLevel);
        //Application.LoadLevel("Genesis");
    }

    
    public void Pause()
    {
        paused = !paused;

        if (paused)
        {
            Debug.Log("Game paused!");
            Time.timeScale = 0;
            ShowButtons();
        }
        else
        {
            Debug.Log("Game resumed!");
            Time.timeScale = 1;
            HideButtons();
        }
    } 

}