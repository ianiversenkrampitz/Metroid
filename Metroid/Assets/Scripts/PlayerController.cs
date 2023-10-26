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
    public float jumpForce = 2f;
    private Rigidbody rigidbodyRef;
    public int playerHealth = 99;
    private bool BallMode = false;
    public bool HasBall = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidbodyRef = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        //move left 
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            HandleJump();
        }
        if (HasBall == true)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                if (BallMode == false)
                {
                    transform.Rotate(Vector3.left * 90);
                    BallMode = true;
                }
                else if (BallMode == true)
                {
                    transform.Rotate(Vector3.left * -90);
                    BallMode = false;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            playerHealth -= 15; 
            if (playerHealth <= 0)
            {
                Die();
            }
        }
        if (other.gameObject.tag == "HardEnemy")
        {
            playerHealth -= 35;
        }
        if (other.gameObject.tag == "Ball")
        {
            HasBall = true;
            other.gameObject.SetActive(false);
        }
    }
    private void HandleJump()
    {
        //detects of player is on the ground 
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.1f))
        {
            //jumps
            Debug.Log("Player is touching ground so jump");
            rigidbodyRef.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        else
        {
            Debug.Log("Player is not touching ground");
        }
    }
    private void Die()
    {
        
    }
}
