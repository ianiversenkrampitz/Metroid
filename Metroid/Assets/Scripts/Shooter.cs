using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Iversen-Krampitz, Ian 
//11/02/2023
//controls the shooter enemy. 
public class Shooter : MonoBehaviour
{
    public GameObject bullets;
    public float spawnRate = 1f;
    public float travelDistanceRight = 0;
    public float travelDistanceLeft = 0;
    public float speed = 3;
    public float startingX;
    private bool movingRight = true;
    public int enemyHealth = 3;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ShootLaser", 0, spawnRate);
    }
   
    // Update is called once per frame
    void Update()
    {
        if (movingRight)
        {
            //if object is not farther that start pos + travel distance, can move right
            if (transform.position.x <= startingX + travelDistanceRight)
            {
                transform.position += Vector3.right * speed * Time.deltaTime;
            }
            else
            {
                movingRight = false;
            }
        }
        else
        {
            //if object is not farther than start pos, can move left 
            if (transform.position.x >= startingX + travelDistanceLeft)
            {
                transform.position += Vector3.left * speed * Time.deltaTime;
            }
            else
            {
                movingRight = true;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //colliding with bullets
        if (other.gameObject.tag == "PlayerBullet")
        {
            //subtracts health 
            enemyHealth -= 1;
            Debug.Log("Shooter took damage.");
            other.gameObject.SetActive(false);
            //if health is zero or less set enemy inactive
            if (enemyHealth <= 0)
            {
                other.gameObject.SetActive(false);
                Destroy(gameObject);
                Debug.Log("Enemy died");
            }
        }
        //colliding with missiles 
        if (other.gameObject.tag == "PlayerMissile")
        {
            //subtracts health 
            enemyHealth -= 5;
            other.gameObject.SetActive(false);
            Debug.Log("Shooter took damage.");
            //if health is zero or less set enemy inactive
            if (enemyHealth <= 0)
            {
                other.gameObject.SetActive(false);
                Destroy(gameObject);
                Debug.Log("Shooter died");
            }
        }
    }
    private void ShootLaser()
    {
        GameObject laserInstance = Instantiate(bullets, transform.position, transform.rotation);
    }
}
