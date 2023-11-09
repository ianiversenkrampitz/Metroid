using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
//Iversen-Krampitz, Ian 
//11/02/2023
//Controls the player and collision. 

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public float jumpForce = 10f;
    private Rigidbody rigidbodyRef;
    public float playerHealth = 99;
    public float maxHealth = 99;
    private bool BallMode = false;
    public bool HasBall = false;
    public bool HasBoots = false;
    public bool HasTank = false;
    public GameObject Bullet;
    public GameObject Missile;
    public GameObject GameOverText;
    public GameObject Explosions;
    public bool hasMissiles = false;
    public bool lookingRight = true;
    private bool takesDamage = true;
    private bool canShoot = true;

    // Start is called before the first frame update
    void Start()
    {
        //gets reference for rigidbody
        rigidbodyRef = GetComponent<Rigidbody>();
        //sets game over text to inactive 
        GameOverText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //move right
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            lookingRight = true;
        }
        //move left 
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
            lookingRight = false;
        }
        //jumps if ball isn't in use
        if (BallMode == false)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                HandleJump();
            }
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
                    //checks if theres room 
                    RaycastHit hit;
                    if (!Physics.Raycast(transform.position, Vector3.up, out hit, 1.01f))
                    {
                        //leaves ball mode if theres enough room 
                        transform.Rotate(Vector3.left * -90);
                        BallMode = false;
                        Debug.Log("Player isn't ball");
                    }
                }
            }
        }
        if (canShoot == true)
        {
            //handles shooting 
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                //shoots if facing left
                if (lookingRight == true)
                {
                    ShootBulletRight();
                    StartCoroutine(ShootDelay());
                }
                //shoots if facing right 
                else if (lookingRight == false)
                {
                    ShootBulletLeft();
                    StartCoroutine(ShootDelay());
                }
            }
        }   
    }
    private void OnCollisionEnter(Collision other)
    {
        if (takesDamage == true)
        {
            //subtracts health when hitting enemy
            if (other.gameObject.tag == "Enemy")
            {
                Debug.Log("Player collided with enemy.");
                playerHealth -= 15;
                StartCoroutine(SetInvincible());
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
                StartCoroutine(SetInvincible());
                //kills player if health is zero
                if (playerHealth <= 0)
                {
                    playerHealth = 0;
                    Die();
                }
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
            Debug.Log("Player gained E tank.");
        }
        //gives player missiles 
        if (other.gameObject.tag == "Missiles")
        {
            hasMissiles = true;
            other.gameObject.SetActive(false);
            Debug.Log("Player gained missiles.");
        }
        if (other.gameObject.tag == "Portal")
        {
            //teleports the player to next portal's position
            transform.position = other.gameObject.GetComponent<Portal>().teleportPoint.transform.position;
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
            else if (maxHealth >= 299)
            {
                playerHealth += 15;
                //sets health to 199 if it goes over max health
                if (playerHealth >= 299)
                {
                    playerHealth = 299;
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
            other.gameObject.SetActive(false);
            Debug.Log("Player receives 15 health.");
        }
        //hurts player with enemy bullets 
        if (other.gameObject.tag == "EnemyBullet")
        {
            if (takesDamage == true)
            {
                Debug.Log("Player collided with bullet.");
                playerHealth -= 15;
                //destroys bullet 
                other.gameObject.SetActive(false);
                StartCoroutine(SetInvincible());
                //kills player if health is zero
                if (playerHealth <= 0)
                {
                    playerHealth = 0;
                    Die();
                }
            }
        }
    }
    /// <summary>
    /// shoots bullets right of the player
    /// </summary>
    private void ShootBulletRight()
    {
        //allows to shoot if not in ball mode
        if (BallMode == false)
        {
            //shoots normal bullets if player doesnt have missiles
            if (hasMissiles == false)
            {
                GameObject bullet = Instantiate(Bullet, transform.position, Bullet.transform.rotation);
                bullet.GetComponent<Bullets>().goingRight = true;
            }
            //shoots missiles if the player has them 
            if (hasMissiles == true)
            {
                GameObject missile = Instantiate(Missile, transform.position, Missile.transform.rotation);
                missile.GetComponent<Bullets>().goingRight = true;
            }
        }   
    }
    /// <summary>
    /// shoots bullets left of the player
    /// </summary>
    private void ShootBulletLeft()
    {
        //allows to shoot if not in ball mode
        if (BallMode == false)
        {
            //shoots normal bullets if player doesnt have missiles
            if (hasMissiles == false)
            { 
                GameObject bullet = Instantiate(Bullet, transform.position, Bullet.transform.rotation);
                bullet.GetComponent<Bullets>().goingRight = false;
            }
            //shoots missiles if the player has them 
            if (hasMissiles == true)
            {
                GameObject missile = Instantiate(Missile, transform.position, Missile.transform.rotation);
                missile.GetComponent<Bullets>().goingRight = false;
            }
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
    /// kills player and makes explosions
    /// </summary>
    private void Die()
    {
        Debug.Log("Player dies.");
        gameObject.SetActive(false);
        //makes explosions
        Instantiate(Explosions, transform.position, Random.rotation);
        Instantiate(Explosions, transform.position, Random.rotation);
        Instantiate(Explosions, transform.position, Random.rotation);
        Instantiate(Explosions, transform.position, Random.rotation);
        Instantiate(Explosions, transform.position, Random.rotation);
        Instantiate(Explosions, transform.position, Random.rotation);

        GameOverText.SetActive(true);
    }
    /// <summary>
    /// sets player invincible for 3 seconds 
    /// </summary>
    /// <returns></returns>
    IEnumerator SetInvincible()
    {
        takesDamage = false;
        StartCoroutine(Blink());
        yield return new WaitForSeconds(3);
        takesDamage = true;
    }
    /// <summary>
    /// delays the player from spamming bullets
    /// </summary>
    /// <returns></returns>
    IEnumerator ShootDelay()
    {
        canShoot = false;
        yield return new WaitForSeconds(.2f);
        canShoot = true;
    }
    /// <summary>
    /// makes the player blink
    /// </summary>
    /// <returns></returns>
    IEnumerator Blink()
    {
        for (int index = 0; index < 30; index++)
        {
            if (index % 2 == 0)
            {
                GetComponent<MeshRenderer>().enabled = false;
            }
            else
            {
                GetComponent<MeshRenderer>().enabled = true;
            }
            yield return new WaitForSeconds(.1f);
        }
        GetComponent<MeshRenderer>().enabled = true;
    }
}
