using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    private PlayerController playercontroller;
    private bool isCarrying =false;
    public float radius=1;
    private Transform actualObjectCarried;

    private void Start()
    {
        playercontroller = GetComponent<PlayerController>();
    }
    void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            Interact();
        }

        if (Input.GetButtonDown("Interact2"))
        {
            Interact();
        }
    }
    private void Interact()
    {
        foreach (Collider hitcol in Physics.OverlapSphere(transform.position+transform.forward +Vector3.up, radius))
        {
            if (hitcol.CompareTag("Carryable") &&!isCarrying)
            {
                CarryingObject(hitcol.transform);
            }
        }
        if(isCarrying)
        {
            ThrowObject();
        }
    }
    private void CarryingObject(Transform carryied)
    {
        carryied.transform.position = transform.position + transform.forward;
        carryied.transform.parent = transform;
        actualObjectCarried = carryied;
        isCarrying = true;
    }
    private void ThrowObject()
    {
        if (actualObjectCarried != null)
        {
            actualObjectCarried.transform.parent = null;
            actualObjectCarried = null;
            isCarrying = false;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.forward+Vector3.up, radius);
    }
}
