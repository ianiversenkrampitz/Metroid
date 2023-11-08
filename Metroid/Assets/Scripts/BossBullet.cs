using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Iversen-Krampitz, Ian 
//11/07/2023
//controls boss bullets. 

public class BossBullet : MonoBehaviour
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
        //makes bullet go left
        transform.position += Vector3.left * speed * Time.deltaTime;
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
