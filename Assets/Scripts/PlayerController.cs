using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public bool isMoving;
    public float rotationSpeed = 0.5f;
    Quaternion lastRotation;
    public float moveSpeed = 3f;
    public bool AuthorizedToMove = true;
    public int playerNumber = 1;
    float xInput;
    float yInput;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (AuthorizedToMove)
        {
            Movement();
        }
    }

    void Movement()
    {
        InputDetection();
        //Calculating Direction
        Vector3 lookDirection = new Vector3(xInput, 0f, yInput); // direction du joystick gauche
                                                                 //   Vector3 lookDirection2 = new Vector3(xInput2, 0f, yInput2); // direction du joystick droit

        //Calculating Animation 
        float animSpeed = Mathf.Abs(Input.GetAxis("Horizontal")) + Mathf.Abs(Input.GetAxis("Vertical"));

        if (xInput >= 0.1f || xInput <= -0.1f || yInput >= 0.1f || yInput < -0.1f) // si le joueur bouge mais ne dash pas
        {
            isMoving = true;//il bouge
            rb.velocity = new Vector3(xInput, 0f, yInput).normalized * moveSpeed;  // la vitesse du joueur est de 0

            Quaternion smoothRotation = Quaternion.LookRotation(lookDirection);
            //transform.rotation = Quaternion.LookRotation(looksDirection, Vector3.up); // le joueur regarde en face de lui
            transform.rotation = Quaternion.Slerp(lastRotation, smoothRotation, rotationSpeed);


            lastRotation = transform.rotation; //Enregistre le dernier input du joueur pour qu'il regarde dans la dernière direction dans laquelle il allait
        }

        else  // si le joueur ne bouge pas
        {
            isMoving = false;
            rb.velocity = new Vector3(0f, 0f, 0f);  // la vitesse du joueur est de 0
            transform.rotation = lastRotation; // le joueur regarde dans la dernière direction enregistrée
        }
    }
    private void InputDetection()
    {
        if (playerNumber == 1)
        {
            //Input Logic
            xInput = Input.GetAxis("Horizontal"); //Joystick gauche horizontal
            yInput = Input.GetAxis("Vertical"); //Joystick gauche vertical
        }
        if (playerNumber == 2)
        {
            //Input Logic
            xInput = Input.GetAxis("Horizontal2"); //Joystick gauche horizontal
            yInput = Input.GetAxis("Vertical2"); //Joystick gauche vertical
        }
    }
}

