using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Iversen-Krampitz, Ian 
//10/31/2023
//Controls the hard enemy. 

public class HardEnemy : MonoBehaviour
{
    public float travelDistanceRight = 0;
    public float travelDistanceLeft = 0;
    public float speed = 3;
    public float startingX;
    private bool movingRight = true;
    public float playerX;
    public GameObject Player;
    public int hardEnemyHealth = 10;

    // Update is called once per frame
    void Update()
    {
        //get player's x position 
        playerX = Player.transform.position.x;
        //moves right if player is to the right of enemy 
        if (playerX >= transform.position.x)
        {
            movingRight = true;
        }
        //moves player left 
        else
        {
            movingRight = false;
        }
        if (movingRight == true)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        if (movingRight == false)
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        //colliding with bullets
        if (other.gameObject.tag == "PlayerBullet")
        {
            //subtracts health 
            hardEnemyHealth -= 1;
            Debug.Log("Enemy took damage.");
            other.gameObject.SetActive(false);
            //if health is zero or less set enemy inactive
            if (hardEnemyHealth <= 0)
            {
                other.gameObject.SetActive(false);
                gameObject.SetActive(false);
                Debug.Log("Enemy died");
            }
        }
        //colliding with missiles 
        if (other.gameObject.tag == "PlayerMissile")
        {
            //subtracts health 
            hardEnemyHealth -= 5;
            other.gameObject.SetActive(false);
            Debug.Log("Enemy took damage.");
            //if health is zero or less set enemy inactive
            if (hardEnemyHealth <= 0)
            {
                other.gameObject.SetActive(false);
                gameObject.SetActive(false);
                Debug.Log("Enemy died");
            }
        }
    }
}
