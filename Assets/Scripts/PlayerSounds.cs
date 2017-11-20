using UnityEngine;
using System.Collections;

public class PlayerSounds : MonoBehaviour
{
    //Player sounds
    public AudioClip runningSound;
    public AudioClip jumpingSound;
    public AudioClip landingSound;
    public AudioClip grunt;

    public PlayerController2D isGrounded;

    private AudioSource source;
    private float volLowRange = .5f;
    private float volHighRange = 1.0f;

    void Awake()
    {
        source = GetComponent<AudioSource>();
    }
    void Update()
    {
        var spearBehaviour = FindObjectOfType<SpearBehaviour>(); //We need to access the hasCollided boolean value to play grunt sound
        if (spearBehaviour.hasCollided == true)
        {
            float vol = Random.Range(volLowRange, volHighRange);
            source.PlayOneShot(grunt, vol);
        }

        if (Input.GetButtonDown("Horizontal") && Input.GetAxisRaw("Horizontal") == 1) //If player runs to right, play sound
        {
            Debug.Log("Running...");
            float vol = Random.Range(volLowRange, volHighRange);
            source.PlayOneShot(runningSound, vol);
        }

        if (Input.GetButtonDown("Horizontal") && Input.GetAxisRaw("Horizontal") == -1) //If player runs to left, play sound
        {
            Debug.Log("Running...");
            float vol = Random.Range(volLowRange, volHighRange);
            source.PlayOneShot(runningSound, vol);
        }

        if (Input.GetKeyDown(KeyCode.Space)) //If player jumps up
        {
            float vol = Random.Range(volLowRange, volHighRange);
            source.PlayOneShot(jumpingSound, vol);

            //For landing sound we will have to use collison to detect if player is on ground
        }

        if (Input.GetButtonUp("Horizontal"))
        {
            source.Stop();
            if (isGrounded == true)
            {
                Debug.Log("Stop!");
            }
        }
    }
}