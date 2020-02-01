﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneInteractions : MonoBehaviour
{
    private PlayerController playercontroller;
    private bool isCarrying = false;
    public float radius = 1;
    private Transform actualObjectCarried;
    private bool canThrow = false;
    public bool isInCraftRange;
    public GameObject nearCraftObject;

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
        if(carryied.GetComponent<FloatingEffect>() != null)
        {
            carryied.GetComponent<FloatingEffect>().enabled = false;
        }
        carryied.GetComponent<ObjectsAttributes>().isCarryied = true;
        carryied.transform.position = transform.position + transform.forward;
        carryied.transform.parent = transform;
        actualObjectCarried = carryied;
        isCarrying = true;
        Invoke("ThrowCooldown",0.01f);
    }
    private void ThrowObject()
    {
        if (actualObjectCarried != null && canThrow &&!isInCraftRange)
        {
            if (actualObjectCarried.GetComponent<FloatingEffect>() != null)
            {
                actualObjectCarried.GetComponent<FloatingEffect>().enabled = true;
                actualObjectCarried.GetComponent<FloatingEffect>().posOffset = actualObjectCarried.transform.position;
            }
            actualObjectCarried.GetComponent<ObjectsAttributes>().isCarryied = false;
            actualObjectCarried.transform.parent = null;
            actualObjectCarried = null;
            isCarrying = false;
            canThrow = false;
        }
        if(isInCraftRange)
        {
            if(nearCraftObject.GetComponent<OvenBehaviour>() != null)
            {
                if (nearCraftObject.GetComponent<OvenBehaviour>().isActivated)
                {
                    isCarrying = false;
                    canThrow = false;
                    nearCraftObject.GetComponent<OvenBehaviour>().actualPlayerUsingOven = this.gameObject;
                    nearCraftObject.GetComponent<OvenBehaviour>().StartBurning();
                    Destroy(actualObjectCarried.gameObject);
                    GetComponent<PlayerController>().AuthorizedToMove = false;
                }
            }
            if (nearCraftObject.GetComponent<FurnaceBehaviour>() != null && actualObjectCarried.GetComponent<ObjectsAttributes>().isCharcoal)
            {
                isCarrying = false;
                canThrow = false;
                Destroy(actualObjectCarried.gameObject);
                nearCraftObject.GetComponent<FurnaceBehaviour>().ResetCoolDown();
            }
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
