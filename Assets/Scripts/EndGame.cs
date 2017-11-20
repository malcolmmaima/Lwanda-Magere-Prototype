using UnityEngine;
using System.Collections;

public class EndGame : MonoBehaviour {

    private bool showPopUp;
    public bool hasFinished;

    void start()
    {
        hasFinished = false;
    }

    void OnCollisionEnter2D(Collision2D Invisible_barrier)
    {
        //If player collides with imaginary barrier gameObject, End of Level 1
        if (Invisible_barrier.gameObject.name == "Player")
        {
            Debug.Log("End of Game!");       
            //Disable player
            var playerController = FindObjectOfType<PlayerController2D>();
            playerController.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

            //Disable buttons here
            playerController.PauseButton.enabled = false;
            playerController.runL.enabled = false;
            playerController.runR.enabled = false;
            playerController.jump.enabled = false;

            //Show pop up message box
            showPopUp = true;
            hasFinished = true;
        }
    }


    void OnGUI()
    {
        if (showPopUp)
        {
            GUI.Window(0, new Rect((Screen.width / 2) - 150, (Screen.height / 2) - 75
                   , 300, 150), ShowGUI, "CONGRATULATIONS!!");

        }
    }

    void ShowGUI(int windowID)
    {
        // You may put a label to show a message to the player

        GUI.Label(new Rect(100, 40, 200, 30), "You have managed");
        GUI.Label(new Rect(100, 60, 200, 30), "to finish level one");
        //Freeze player mechanism here
       
        // slot 1: moves L & R, slot 2: moves up & down, slot 3: +/- length, slot 4: +/- width
        if (GUI.Button(new Rect(80, 100, 75, 30), "MENU")) 
        {
            showPopUp = false;
            Application.LoadLevel("Menu");
        }

        if (GUI.Button(new Rect(160, 100, 75, 30), "NEXT"))
        {
            showPopUp = false;
            Debug.Log("Load next level...");
            Application.LoadLevel("Genesis_2");
        }
    }
}

