using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardEnemy : MonoBehaviour
{
    public float travelDistanceRight = 0;
    public float travelDistanceLeft = 0;
    public float speed = 3;
    private float startingX;
    private bool movingRight = true;
    public float playerX;
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

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
}
