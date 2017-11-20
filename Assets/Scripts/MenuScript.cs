using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {

    public void StartGame()
    {
        // "Genesis" is the name of the first scene we created.
        Application.LoadLevel("Genesis");
        Debug.Log("Begin!");
    }

    public void HelpGame()
    {
        //Instructions on playing the game, Credits, Controls, references etc. Will be a scene on its own.
        Debug.Log("Confused a little bit!?");
        Application.LoadLevel("Help");
    }

    public void GoBackMenu()
    {
        Debug.Log("Back to Menu...");
        Application.LoadLevel("Menu");
    }
    public void EndGame()
    {
        //On pressing Exit button game exits completely
        Application.Quit();
        Debug.Log("Game Exit!");
    }
}
