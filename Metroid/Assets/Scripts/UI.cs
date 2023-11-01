using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//Iversen-Krampitz, Ian 
//10/31/2023
//Controls the UI. 

public class UI : MonoBehaviour
{
    public TMP_Text Health;
    public PlayerController playerController;

    // Update is called once per frame
    void Update()
    {
        Health.text = "Energy: " + playerController.playerHealth + " of " + playerController.maxHealth;
    }
}
