using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//Iversen-Krampitz, Ian 
//11/07/2023
//controls boss. 

public class Boss : MonoBehaviour
{
    public GameObject Laser;
    public GameObject LaserSpawner;
    public GameObject Explosions;
    public float spawnRate = 1;
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
                //destroys boss and makes explosion effect
                other.gameObject.SetActive(false);
                GameObject explosion1 = Instantiate(Explosions, LaserSpawner.transform.position, Random.rotation);
                GameObject explosion2 = Instantiate(Explosions, LaserSpawner.transform.position, Random.rotation);
                GameObject explosion3 = Instantiate(Explosions, LaserSpawner.transform.position, Random.rotation);
                GameObject explosion4 = Instantiate(Explosions, LaserSpawner.transform.position, Random.rotation);
                GameObject explosion5 = Instantiate(Explosions, LaserSpawner.transform.position, Random.rotation);
                GameObject explosion6 = Instantiate(Explosions, LaserSpawner.transform.position, Random.rotation);
                GameObject explosion7 = Instantiate(Explosions, LaserSpawner.transform.position, Random.rotation);
                GameObject explosion8 = Instantiate(Explosions, LaserSpawner.transform.position, Random.rotation);
                GameObject explosion9 = Instantiate(Explosions, LaserSpawner.transform.position, Random.rotation);
                GameObject explosion10 = Instantiate(Explosions, LaserSpawner.transform.position, Random.rotation);
                Destroy(gameObject);
                Destroy(LaserSpawner);
                Debug.Log("Boss died");
                //delays before moving to end screen
                StartCoroutine(EndDelay());
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
                //destroys boss and makes explosion effect
                other.gameObject.SetActive(false);
                GameObject explosion1 = Instantiate(Explosions, LaserSpawner.transform.position, Random.rotation);
                GameObject explosion2 = Instantiate(Explosions, LaserSpawner.transform.position, Random.rotation);
                GameObject explosion3 = Instantiate(Explosions, LaserSpawner.transform.position, Random.rotation);
                GameObject explosion4 = Instantiate(Explosions, LaserSpawner.transform.position, Random.rotation);
                GameObject explosion5 = Instantiate(Explosions, LaserSpawner.transform.position, Random.rotation);
                GameObject explosion6 = Instantiate(Explosions, LaserSpawner.transform.position, Random.rotation);
                GameObject explosion7 = Instantiate(Explosions, LaserSpawner.transform.position, Random.rotation);
                GameObject explosion8 = Instantiate(Explosions, LaserSpawner.transform.position, Random.rotation);
                GameObject explosion9 = Instantiate(Explosions, LaserSpawner.transform.position, Random.rotation);
                GameObject explosion10 = Instantiate(Explosions, LaserSpawner.transform.position, Random.rotation);
                Destroy(gameObject);
                Destroy(LaserSpawner);
                Debug.Log("Boss died");
                //delays before moving to end screen
                StartCoroutine(EndDelay());
            }
        }
    }
    //fires lasers
    private void ShootLaser()
    {
        GameObject laserInstance = Instantiate(Laser, transform.position, Laser.transform.rotation);
    }
    /// <summary>
    /// lets you see the boss die before going to end screen
    /// </summary>
    IEnumerator EndDelay()
    {
        Debug.Log("waiting to end");
        yield return new WaitForSeconds(5f);
        Debug.Log("game ended.");
        SceneManager.LoadScene("EndScreen");
    }    
}
