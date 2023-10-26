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
    private bool BallMode = false;
    public bool HasBall = false;
    public bool HasBoots = false;
    public bool HasTank = false;
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

    private void OnTriggerEnter(Collider other)
    {
        //subtracts health when hitting enemy
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("Player collided with enemy.");
            playerHealth -= 15; 
        }
        //subtracts health when hitting hard enemy
        if (other.gameObject.tag == "HardEnemy")
        {
            Debug.Log("Player collided with hard enemy.");
            playerHealth -= 35;
        }
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
        //kills player if health is zero
        if (playerHealth <= 0)
        {
            Die();
        }
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
    }
}
