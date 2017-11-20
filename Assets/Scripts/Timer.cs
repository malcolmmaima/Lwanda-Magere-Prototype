using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    #region Public Variables
    public int timeLeft = 30;
    public Text countdownText;
    public Text PopUpText;
    public PlayerController2D pcontroller;
    public EndGame endGame;
    #endregion //Public variables

    #region Main Methods
    // Use this for initialization
    void Start()
    {
        StartCoroutine("LoseTime");
        PopUpText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        countdownText.text = ("Timer: " + timeLeft + "s");

        if(endGame.hasFinished == true) //If the player has finished the terrain before the timer times out, then stop timer
        {
            StopCoroutine("LoseTime");
            pcontroller.Die();
        }
       
            if (timeLeft <= 0)
            {
                StopCoroutine("LoseTime");
                PopUpText.enabled = true;
                PopUpText.text = "Times Up!";
                countdownText.text = "Retry";
                pcontroller.currentPlayerHealth = 0; //Reset our player's health to 0 since he has not beaten the timer
                pcontroller.Die(); //We call the Die method in player controller (our player dies)
                
            }
        }
    #endregion //Main methods

    IEnumerator LoseTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            timeLeft--;
        }
    }
}
