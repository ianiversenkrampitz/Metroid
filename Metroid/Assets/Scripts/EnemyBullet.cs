using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Iversen-Krampitz, Ian 
//10/20/2023
//Controls the bullet. 

public class EnemyBullet : MonoBehaviour
{
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DespawnDelay());
    }

    // Update is called once per frame
    void Update()
    {
        //makes bullet go up 
        transform.position += Vector3.up * speed * Time.deltaTime;
    }

    /// <summary>
    /// wait three seconds then destroy self 
    /// </summary>
    /// <returns></returns>
    IEnumerator DespawnDelay()
    {
        yield return new WaitForSeconds(7);
        Destroy(this.gameObject);
    }
}
