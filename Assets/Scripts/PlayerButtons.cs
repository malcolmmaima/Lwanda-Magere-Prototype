using UnityEngine;
using UnityEngine.UI;

// Start or quit the game

public class PlayerButtons : MonoBehaviour
{
    private Button[] buttons;

    
    void Awake()
    {
        // Get the buttons
        buttons = GetComponentsInChildren<Button>();

        // Enable them
        ShowButtons();
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

    public void Run_right()
    {
        Debug.Log("Run right");
        //Right move button clicked/touched do something...  
    }

    public void Run_left()
    {
       Debug.Log("Run left");
        //Left move button clicked/touched do something...
    }

    public void Jump()
    {
        Debug.Log("Jump up button pressed!");
    }

}