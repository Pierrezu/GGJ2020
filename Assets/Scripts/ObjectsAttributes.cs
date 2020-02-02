using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsAttributes : MonoBehaviour
{
    public bool isCarryied = false;
    public bool isCharcoal;
    public bool isABattery;
    public bool isABatteryRepaired;
    private void OnCollisionEnter(Collision other)
    {
        if (isABattery)
        {
            if (GetComponent<Rigidbody>() != null)
            {
                GetComponent<Rigidbody>().isKinematic = true;
                GetComponent<Collider>().isTrigger = true;
            }
        }
    }
}
