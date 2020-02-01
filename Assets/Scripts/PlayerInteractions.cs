using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    private PlayerController playercontroller;
    private bool isCarrying =false;
    public float radius=1;

    private void Start()
    {
        playercontroller = GetComponent<PlayerController>();
    }
    void Update()
    {
        if (Input.GetButtonDown("Interact") && !isCarrying)
        {
            Interact();
        }

        if (Input.GetButtonDown("Interact2")&& !isCarrying)
        {
            Interact();
        }
    }
    private void Interact()
    {
        foreach (Collider hitcol in Physics.OverlapSphere(transform.forward +Vector3.up, radius))
        {
            if(hitcol.CompareTag("Carryable"))
            {
                //dosomething
                isCarrying = true;
            }
        }
    }
    private void CarryingObject()
    {
        
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.forward+Vector3.up, radius);
    }
}
