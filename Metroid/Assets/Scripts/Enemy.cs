using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Iversen-Krampitz, Ian 
//10/26/2023
//controls normal enemy movement. 

public class Enemies : MonoBehaviour
{
    public float travelDistanceRight = 0;
    public float travelDistanceLeft = 0;
    public float speed = 3;
    private float startingX;
    private bool movingRight = true;
    public int enemyHealth = 1;

    // Start is called before the first frame update
    void Start()
    {
        //stores initial x value of object
        startingX = transform.position.x;
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
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "PlayerBullet")
        {
            enemyHealth -= 1;
            other.gameObject.SetActive(false);
        }
        if (enemyHealth <= 0)
        {
            other.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
        if (other.gameObject.tag == "PlayerMissile")
        {
            enemyHealth -= 5;
            other.gameObject.SetActive(false);
        }
        if (enemyHealth <= 0)
        {
            other.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}