using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Iversen-Krampitz, Ian 
//11/17/2023
//controls missile wall. 
public class MissileWall : MonoBehaviour
{
    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }
    private void Update()
    {
        //workaround to fix player clipping through object
        transform.position = startPos;
    }
    public void OnTriggerEnter(Collider other)
    {
        //destroys wall when hitting missile 
        if (other.gameObject.tag == "PlayerMissile")
        {
            other.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
