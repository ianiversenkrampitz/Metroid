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
    private float startingX;
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
        if (other.gameObject.tag == "PlayerBullet")
        {
            hardEnemyHealth -= 1;
            other.gameObject.SetActive(false);
        }
        if (hardEnemyHealth <= 0)
        {
            other.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
        if (other.gameObject.tag == "PlayerMissile")
        {
            hardEnemyHealth -= 5;
            other.gameObject.SetActive(false);
        }
        if (hardEnemyHealth <= 0)
        {
            other.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
