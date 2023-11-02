using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Iversen-Krampitz, Ian 
//11/02/2023
//controls missile wall. 
public class MissileWall : MonoBehaviour
{
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
