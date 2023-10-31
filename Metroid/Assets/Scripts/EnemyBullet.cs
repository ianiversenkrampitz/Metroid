using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Iversen-Krampitz, Ian 
//10/20/2023
//Controls the bullet. 

public class EnemyBullet : MonoBehaviour
{
    public float speed;
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
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        //if going right is false, go left 
        else
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
    }
    /// <summary>
    /// wait three seconds then destroy self 
    /// </summary>
    /// <returns></returns>
    IEnumerator DespawnDelay()
    {
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);
    }
}
