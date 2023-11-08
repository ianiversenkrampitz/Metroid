using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//Iversen-Krampitz, Ian 
//11/08/2023
//Controls the end level portal when boss is killed. 

public class EndLevel : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            SceneManager.LoadScene("EndScreen");
        }
    }
}
