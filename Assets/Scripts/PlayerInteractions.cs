using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    private PlayerController playercontroller;

    private void Start()
    {
        playercontroller = GetComponent<PlayerController>();
    }
    void Update()
    {
        if(playercontroller.playerNumber == 1)
        {
            if (Input.GetButtonDown("Interact"))
            {
                Debug.Log(playercontroller.playerNumber + "st player interact");
            }
        }
        if (playercontroller.playerNumber == 2)
        {
            if (Input.GetButtonDown("Interact2"))
            {
                Debug.Log(playercontroller.playerNumber + "nd player interact");
            }
        }
    }
    private void Interact()
    {

    }
}
