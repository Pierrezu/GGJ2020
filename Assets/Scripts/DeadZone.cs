using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Carryable") && other.GetComponent<ObjectsAttributes>() != null)
        {
            if (other.GetComponent<ObjectsAttributes>().isABattery== false)
            {
                Destroy(other.gameObject);
            }
        }
    }
}
