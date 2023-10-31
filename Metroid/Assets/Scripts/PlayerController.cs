using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//Iversen-Krampitz, Ian 
//10/24/2023
//Controls the player and collision. 

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public float jumpForce = 10f;
    private Rigidbody rigidbodyRef;
    public int playerHealth = 99;
    public int maxHealth = 99;
    private bool BallMode = false;
    public bool HasBall = false;
    public bool HasBoots = false;
    public bool HasTank = false;
    public GameObject Bullet;
    public GameObject Missile;
    public bool hasMissiles = false;

    // Start is called before the first frame update
    void Start()
    {
        //gets reference for rigidbody
        rigidbodyRef = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //move right
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        //move left 
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        //jumps
        if (Input.GetKeyDown(KeyCode.Space))
        {
            HandleJump();
        }
        //crouches into ball 
        if (HasBall == true)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                if (BallMode == false)
                {
                    transform.Rotate(Vector3.left * 90);
                    BallMode = true;
                    Debug.Log("Player is ball");
                }
                else if (BallMode == true)
                {
                    transform.Rotate(Vector3.left * -90);
                    BallMode = false;
                    Debug.Log("Player isn't ball");
                }
            }
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        //subtracts health when hitting enemy
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("Player collided with enemy.");
            playerHealth -= 15;
            //kills player if health is zero
            if (playerHealth <= 0)
            {
                playerHealth = 0;
                Die();
            }
        }
        //subtracts health when hitting hard enemy
        if (other.gameObject.tag == "HardEnemy")
        {
            Debug.Log("Player collided with hard enemy.");
            playerHealth -= 35;
            //kills player if health is zero
            if (playerHealth <= 0)
            {
                playerHealth = 0;
                Die();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //gives player ability when colliding with ball powerup
        if (other.gameObject.tag == "Ball")
        {
            HasBall = true;
            other.gameObject.SetActive(false);
            Debug.Log("Player gained Morph Ball ability.");
        }
        //gives player ability when colliding with boots powerup
        if (other.gameObject.tag == "Boots")
        {
            HasBoots = true;
            other.gameObject.SetActive(false);
            Debug.Log("Player gained High Jump ability.");
        }
        //gives player +100 max health 
        if(other.gameObject.tag == "Etank")
        {
            maxHealth += 100;
            playerHealth += 100;
            other.gameObject.SetActive(false);
        }
        //gives player 15 health from health pack
        if (other.gameObject.tag == "Health")
        {
            //checks if player picked up E tank
            if (maxHealth == 199)
            {
                playerHealth += 15;
                //sets health to 199 if it goes over max health 
                if (playerHealth >= 199)
                {
                    playerHealth = 199;
                }
            }
            else if (maxHealth == 99)
            {
                playerHealth += 15;
                //sets health to 199 if it goes over max health
                if (playerHealth >= 99)
                {
                    playerHealth = 99;
                }
            }     
        }
    }
    /// <summary>
    /// shoots bullets right of the player
    /// </summary>
    private void ShootBulletRight()
    {
        if (BallMode == false)
        {
            if (hasMissiles == false)
            {
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    GameObject bullet = Instantiate(Bullet, transform.position, transform.rotation);
                    bullet.GetComponent<EnemyBullet>().goingRight = true;
                }
            }
            if (hasMissiles == true)
            {
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {

                }
            }
        }   
    }
    /// <summary>
    /// shoots bullets left of the player
    /// </summary>
    private void ShootBulletLeft()
    {

    }
    /// <summary>
    /// makes player jump
    /// </summary>
    private void HandleJump()
    {
        //detects if player is on the ground 
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.1f))
        {
            if (HasBoots == true)
            {
                //does high jump
                Debug.Log("Player is touching ground so jump");
                rigidbodyRef.AddForce(Vector3.up * (jumpForce + 3f), ForceMode.Impulse);
            }
            else if (HasBoots == false)
            {
                //jumps normally
                Debug.Log("Player is touching ground so jump");
                rigidbodyRef.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
        else
        {
            Debug.Log("Player is not touching ground");
        }
    }
    /// <summary>
    /// kills player
    /// </summary>
    private void Die()
    {
        Debug.Log("Player dies.");
        //SceneManager.LoadScene(Scene2);
    }
}
