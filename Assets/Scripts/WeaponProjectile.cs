using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WeaponProjectile : MonoBehaviour
{
    public Rigidbody2D projectile;
    public Transform projectileSpawnPoint;
    public float projectileVelocity;
    public float timeBetweenShots;
    private float timeBetweenShotsCounter;
    private bool canShoot;
    // Use this for initialization
    void Start()
    {
        canShoot = false;
        timeBetweenShotsCounter = timeBetweenShots;
    }

    void OnCollisionEnter2D(Collision2D Checkpoint)
    {
        //If player collides with imaginary checkpoint gameObject,shoot random arrows towards him
        if (Checkpoint.gameObject.name == "Player")
        {
            Debug.Log("Incoming spear...");
            Rigidbody2D spearInstance = Instantiate(projectile, projectileSpawnPoint.position, Quaternion.Euler(new Vector3(1, 0, transform.localEulerAngles.z))) as Rigidbody2D;
            spearInstance.GetComponent<Rigidbody2D>().AddForce(projectileSpawnPoint.right * projectileVelocity);
            canShoot = false;
        }
        if (!canShoot)
        {
            timeBetweenShotsCounter -= Time.deltaTime;
            if (timeBetweenShotsCounter <= 0)
            {
                canShoot = true;
                timeBetweenShotsCounter = timeBetweenShots;
            }
        }

    }

    // Update is called once per frame
    /*
     * For testing purposes
     * 
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canShoot)
        {
            Rigidbody2D bulletInstance = Instantiate(projectile, projectileSpawnPoint.position, Quaternion.Euler(new Vector3(0, 0, transform.localEulerAngles.z))) as Rigidbody2D;
            bulletInstance.GetComponent<Rigidbody2D>().AddForce(projectileSpawnPoint.right * projectileVelocity);
            canShoot = false;
        }
        if (!canShoot)
        {
            timeBetweenShotsCounter -= Time.deltaTime;
            if (timeBetweenShotsCounter <= 0)
            {
                canShoot = true;
                timeBetweenShotsCounter = timeBetweenShots;
            }
        }
    } */
}

