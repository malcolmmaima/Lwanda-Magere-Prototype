using UnityEngine;
using System.Collections;

public class SpearBehaviour : MonoBehaviour {
    
    public bool hasCollided; //Helps us pass a message to PlayerController2D that an arrow has collided with player

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        hasCollided = false; //We need to constantly reset this back to false, since we have multiple arrows
    }

    void OnCollisionEnter2D(Collision2D spear)
    {
        
        //If spear collides with player, suck health outta that fake a** warrrior
        if (spear.gameObject.name == "Player")
        {
            Debug.Log("Health -2 points");
            hasCollided = true;
        }


    }
}
