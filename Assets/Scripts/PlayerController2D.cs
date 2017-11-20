using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController2D : MonoBehaviour
{

    public int moveSpeed;
    public int jumpHeight;
    public int extraJumps;
    public int currentPlayerHealth;
    public int maxPlayerHealth;
    public Image healthBar;
    public Text HealthScore;
    public GameObject graphics;
    public Transform groundPoint;
    public float radius;
    public LayerMask groundMask;
    public Transform SpawnPoint; //Where our player will be placed on respawn 

    //Canvas Buttons: For the purpose of disabling and enabling them
    public Button PauseButton, runL, runR, jump;
    private bool showPopUp;
    bool isGrounded;
    Rigidbody2D rb2D;
    int originalExtraJumps;

    void Start()
    {

    }

    void Awake(){
        
        rb2D = GetComponent<Rigidbody2D>(); // Our player's Rigidbody 2D
        originalExtraJumps = extraJumps;
        currentPlayerHealth = maxPlayerHealth;

        //Show pop up message box
        showPopUp = true;
    }

    //Player collision calculations and actions
    void OnCollisionEnter2D(Collision2D player) {
        //If player collides with imaginary destroyer gameObject, kill that black man! ASAP!
        if (player.gameObject.name == "Destroy")
        {
            Debug.Log("You're dead");
            currentPlayerHealth = currentPlayerHealth - 10;
            Die(); // We will have to work on this abit more by creating a respawn mechanism and gamescore save
        }
    }


    void Update()
    { //Update executes per frame
        
        healthBar.fillAmount = (float)currentPlayerHealth / maxPlayerHealth; // This fills/unfills the healthbar
        var spearBehaviour = FindObjectOfType<SpearBehaviour>(); //We need to access the hasCollided boolean value to determine health damage
        if (spearBehaviour.hasCollided == true)
        {
            Debug.Log("Arrow has collided with player!");
            currentPlayerHealth = currentPlayerHealth - 2; //Deduct 2points everytime arrow hits player
        }
        

        //Just health interactive stuff
        //Color code references: https://docs.unity3d.com/ScriptReference/Color.html
        if (currentPlayerHealth <= 0)
        {
            HealthScore.text = "No Health!";
        }

        else if (currentPlayerHealth <= 20)
        {
            HealthScore.color = new Color(1, 0, 0, 1); //Red
            HealthScore.text = "Danger: " + currentPlayerHealth.ToString();  
        }

        else
        {
            HealthScore.color = new Color(0, 0, 0, 1); //Black
            HealthScore.text = "Health: " + currentPlayerHealth.ToString();
        }
        // End of health interactive stuff

        Vector2 moveDir = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, rb2D.velocity.y);
        rb2D.velocity = moveDir;

        isGrounded = Physics2D.OverlapCircle(groundPoint.position, radius, groundMask); //Returns true or false

        if (Input.GetAxisRaw("Horizontal") == 1) {
            transform.localScale = new Vector3(1, 1, 1); //Face Right direction 
        }
        else if (Input.GetAxisRaw("Horizontal") == -1) {
            transform.localScale = new Vector3(-1, 1, 1); //Face Left direction 
        }

        if (Input.GetAxisRaw("Horizontal") != 0) // If player is moving, set moving animation
        {
            graphics.GetComponent<Animator>().SetBool("isMoving", true);
            Debug.Log("is moving...");
        }
        else {
            graphics.GetComponent<Animator>().SetBool("isMoving", false);
            Debug.Log("not moving...");
        }

        if (Input.GetKeyDown(KeyCode.Space)) { // If jump button is pressed
            Jump();
        }

        if (isGrounded) {
            Debug.Log("On ground...");
            extraJumps = originalExtraJumps;
            graphics.GetComponent<Animator>().SetBool("isJump", false); //Set jump animation on hitting ground
        }

        if (currentPlayerHealth <= 0) {
            Debug.Log("Low Health!");
            Die();
        }

        //Our player phone touch control mechanism
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Stationary)
        {
            Vector2 touchPosition = Input.GetTouch(0).position;
            double halfScreen = Screen.width / 2.0;

            //Check if touch is on left or right?
            if (touchPosition.x < halfScreen)
            {
                gameObject.transform.Translate(Vector3.left * moveSpeed * Time.deltaTime); //Move left
                transform.localScale = new Vector3(-1, 1, 1); //Face Left direction
                graphics.GetComponent<Animator>().SetBool("isMoving", true); //Player movement animation
            }

            else if (touchPosition.x > halfScreen)
            {
                gameObject.transform.Translate(Vector3.right * moveSpeed * Time.deltaTime); //Move right
                transform.localScale = new Vector3(1, 1, 1); //Face right direction
                graphics.GetComponent<Animator>().SetBool("isMoving", true); //Player movement animation
            } 
            
            else
            {
                graphics.GetComponent<Animator>().SetBool("isMoving", false);
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundPoint.position, radius);
    }

    public void Jump()
    {
        graphics.GetComponent<Animator>().SetBool("isJump", true);
        Debug.Log("Jump...");
        if (extraJumps != 0)
        {
            rb2D.AddForce(new Vector2(0, jumpHeight));
            extraJumps--;
        }
    }
    
    void Respawn()
    {
        //Set player to last known location...
        this.transform.position = SpawnPoint.transform.position;
    }

    public void Die()
    {

        var gameOver = FindObjectOfType<GameOverScript>();
        Debug.Log("Dead!");

        if(currentPlayerHealth <= 0)
        {
            gameObject.GetComponentInChildren<Renderer>().enabled = false; //Hides the player (makes them invisible)
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll; //Freezes the player
            gameOver.ShowButtons();

            //Disable our player buttons on gameover
            PauseButton.enabled = false; //Fixed bug: Disable pause button on gameover
            runL.enabled = false;
            runR.enabled = false;
            jump.enabled = false;
            Debug.Log("Buttons disabled");
            
            //NOTE FOR FUTURE: Remember to add touch freeze mechanism
        }
        else
        {
            Respawn();
            var a = FindObjectOfType<GameOverScript>();
            a.HideButtons();

            //Re-enable buttons on respwan
            PauseButton.enabled = true;
            runL.enabled = true;
            runR.enabled = true;
            jump.enabled = true;
            Debug.Log("Buttons enabled");

        }
    }

    void OnGUI()
    {
        if (showPopUp)
        {
            GUI.Window(0, new Rect((Screen.width / 2) - 150, (Screen.height / 2) - 75
                   , 300, 180), ShowGUI, "A JOURNEY AWAITS YOU!");

        }
    }

    void ShowGUI(int windowID)
    {
        // You may put a label to show a message to the player
        GUI.Label(new Rect(60, 40, 200, 30), "Hello soldier, you are about to ");
        GUI.Label(new Rect(60, 60, 200, 30), "go on a journey of self discovery");
        GUI.Label(new Rect(60, 80, 200, 30), "Watch out for enemy fire, finish ");
        GUI.Label(new Rect(60, 100, 200, 40), "the intro terrain and get ready to fight.");

        // slot 1: moves L & R, slot 2: moves up & down, slot 3: +/- length, slot 4: +/- width
        if (GUI.Button(new Rect(120, 140, 75, 30), "OK"))
        {
            showPopUp = false;
        }

    }
}
