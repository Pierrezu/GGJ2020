using System.Collections;
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
        if (carryied.GetComponent<FloatingEffect>() != null)
        {
            carryied.GetComponent<FloatingEffect>().enabled = false;
        }
        if (carryied.GetComponent<BatteryBehaviour>() != null)
        {
            carryied.GetComponent<BatteryBehaviour>().isPlugged = false;
            if (carryied.GetComponent<BatteryBehaviour>().linkedPlug != null)
            {
                carryied.GetComponent<BatteryBehaviour>().SetPlugState();
            }
        }
        if(carryied.GetComponent<Rigidbody>()!= null)
        {
            carryied.GetComponent<Rigidbody>().velocity = Vector3.zero;
            carryied.GetComponent<Rigidbody>().isKinematic = true;
        }
        carryied.GetComponent<ObjectsAttributes>().isCarryied = true;
        carryied.transform.position = transform.position + transform.forward;
        carryied.transform.parent = transform;
        actualObjectCarried = carryied;
        isCarrying = true;
        Invoke("ThrowCooldown", 0.01f);
    }
    private void ThrowObject()
    {
        if (actualObjectCarried != null && canThrow && !isInCraftRange)
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
        if (isInCraftRange && actualObjectCarried != null)
        {
            if (nearCraftObject.GetComponent<OvenBehaviour>() != null)
            {
                if (nearCraftObject.GetComponent<OvenBehaviour>().isActivated)
                {

                    if(actualObjectCarried.GetComponent<ObjectsAttributes>().isCharcoal == false && actualObjectCarried.GetComponent<ObjectsAttributes>().isABattery == false && actualObjectCarried.GetComponent<ObjectsAttributes>().isABattery == false)
                    {
                        nearCraftObject.GetComponent<OvenBehaviour>().actualNbOfLingots++;
                        isCarrying = false;
                        canThrow = false;
                        nearCraftObject.GetComponent<OvenBehaviour>().actualPlayerUsingOven = this.gameObject;
                        Destroy(actualObjectCarried.gameObject);
                        nearCraftObject.GetComponent<OvenBehaviour>().StartBurning();
                    }
                    if (actualObjectCarried.GetComponent<ObjectsAttributes>().isABattery && actualObjectCarried.GetComponent<ObjectsAttributes>().isABatteryRepaired == false)
                    {
                        isCarrying = false;
                        canThrow = false;
                        nearCraftObject.GetComponent<OvenBehaviour>().actualPlayerUsingOven = this.gameObject;
                        nearCraftObject.GetComponent<OvenBehaviour>().pileIsInside = true;
                        Destroy(actualObjectCarried.gameObject);
                        nearCraftObject.GetComponent<OvenBehaviour>().StartBurning();
                    }

                    /*  GetComponent<PlayerController>().AuthorizedToMove = false;
                      GetComponent<Rigidbody>().velocity = Vector3.zero;*/
                }
            }
            if (nearCraftObject.GetComponent<FurnaceBehaviour>() != null && actualObjectCarried.GetComponent<ObjectsAttributes>().isCharcoal)
            {
                isCarrying = false;
                canThrow = false;
                Destroy(actualObjectCarried.gameObject);
                nearCraftObject.GetComponent<FurnaceBehaviour>().ResetCoolDown();
            }
            if (nearCraftObject.GetComponent<PlugBehaviour>() != null && actualObjectCarried.GetComponent<ObjectsAttributes>().isABattery)
            {
                if (nearCraftObject.GetComponent<PlugBehaviour>().isActivated == false)
                {
                    actualObjectCarried.GetComponent<BatteryBehaviour>().isPlugged = true;
                    actualObjectCarried.GetComponent<BatteryBehaviour>().linkedPlug = nearCraftObject;
                    actualObjectCarried.GetComponent<ObjectsAttributes>().isCarryied = false;
                    actualObjectCarried.transform.position = nearCraftObject.transform.position;
                    actualObjectCarried.GetComponent<BatteryBehaviour>().SetPlugState();
                    actualObjectCarried.transform.parent = null;
                    actualObjectCarried = null;
                    isCarrying = false;
                    canThrow = false;
                }
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


