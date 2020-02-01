using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTwoInteractions : MonoBehaviour
{
    private PlayerController playercontroller;
    private bool isCarrying = false;
    public float radius = 1;
    private Transform actualObjectCarried;
    private bool canThrow = false;

    private void Start()
    {
        playercontroller = GetComponent<PlayerController>();
    }
    void Update()
    {
        if (Input.GetButtonDown("Interact2"))
        {
            Interact();
        }
    }
    private void Interact()
    {
        foreach (Collider hitcol in Physics.OverlapSphere(transform.position + transform.forward + Vector3.up, radius))
        {
            if (hitcol.CompareTag("Carryable") && !isCarrying && hitcol.GetComponent<ObjectsAttributes>().isCarryied == false)
            {
                CarryingObject(hitcol.transform);
            }
        }
        if (isCarrying)
        {
            ThrowObject();
        }
    }
    private void CarryingObject(Transform carryied)
    {
        carryied.GetComponent<ObjectsAttributes>().isCarryied = true;
        carryied.transform.position = transform.position + transform.forward;
        carryied.transform.parent = transform;
        actualObjectCarried = carryied;
        isCarrying = true;
        Invoke("ThrowCooldown", 0.01f);
    }
    private void ThrowObject()
    {
        if (actualObjectCarried != null && canThrow)
        {
            actualObjectCarried.GetComponent<ObjectsAttributes>().isCarryied = false;
            actualObjectCarried.transform.parent = null;
            actualObjectCarried = null;
            isCarrying = false;
            canThrow = false;
        }
    }
    private void ThrowCooldown()
    {
        canThrow = true;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.forward + Vector3.up, radius);
    }
}

