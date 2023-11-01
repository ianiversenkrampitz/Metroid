using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
//Iversen-Krampitz, Ian 
//10/31/2023
//Controls the player projectiles. 

public class Bullets : MonoBehaviour
{
    public float bulletSpeed;
    public bool goingRight;

    // Start is called before the first frame update
    void Start()
    {
        //starts coroutine when object is instantiated into scene
        StartCoroutine(DespawnDelay());
    }

    // Update is called once per frame
    void Update()
    {
        //if going right is true, go right 
        if (goingRight)
        {
            transform.position += Vector3.right * bulletSpeed * Time.deltaTime;
        }
        //if going right is false, go left 
        else
        {
            transform.position += Vector3.left * bulletSpeed * Time.deltaTime;
        }
    }
    IEnumerator DespawnDelay()
    {
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);
    }
}
