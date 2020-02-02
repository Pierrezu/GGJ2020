using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryBehaviour : MonoBehaviour
{
    public bool isPlugged;
    public GameObject linkedPlug;

    public void SetPlugState()
    {
        if(isPlugged)
        {
            linkedPlug.GetComponent<PlugBehaviour>().Activation();
        }
        if(!isPlugged)
        {
            linkedPlug.GetComponent<PlugBehaviour>().Deactivation();
        }
    }
}
