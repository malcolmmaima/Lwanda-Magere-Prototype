using UnityEngine;
using System.Collections;

public class EnemyController2D : MonoBehaviour {
    public Transform visionPoint;
    private PlayerController2D player;

    public Transform Player;

    public float visionAngle = 30f;
    public float visionDistance = 10f;
    public float moveSpeed = 2f;
    public float chaseDistance = 3f;

    private Vector3? lastKnownPlayerPosition;
    // Use this for initialization
    void Start () {
        player = GameObject.FindObjectOfType<PlayerController2D> ();
    }

    // Update is called once per frame
    void Update () {
        // Not giving the desired results
        transform.LookAt(Player);

        /*
        Vector3 lookAt = Player.position;
		lookAt.y = transform.position.y;
		transform.LookAt(lookAt);
		*/
		
    }

    void FixedUpdate () {

    }

    void Look () {
        Vector3 deltaToPlayer = player.transform.position - visionPoint.position;
        Vector3 directionToPlayer = deltaToPlayer.normalized;

        float dot = Vector3.Dot (transform.forward, directionToPlayer);

        if (dot < 0) {
            return;
        }

        float distanceToPlayer = directionToPlayer.magnitude;

        if (distanceToPlayer > visionDistance)
        {
            return;
        }

        float angle = Vector3.Angle (transform.forward, directionToPlayer);

        if(angle > visionAngle)
        {
            return;
        }

        RaycastHit hit;
        if(Physics.Raycast(transform.position, directionToPlayer, out hit, visionDistance))
        {
            if (hit.collider.gameObject == player.gameObject)
            {
                lastKnownPlayerPosition = player.transform.position;
            }
        }
    }
}