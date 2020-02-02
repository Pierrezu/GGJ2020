using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlugBehaviour : MonoBehaviour
{
    public bool isActivated;
    public GameObject[] linkedObjects;
    public Transform pileAnchor;
    public void Activation()
    {/*
        foreach (GameObject activables in linkedObjects)
        {
            if (activables.GetComponent<DoorBehaviour>() != null)
            {
                activables.GetComponent<DoorBehaviour>().Open();
            }
        }*/
        for (int i = 0; i < linkedObjects.Length; i++)
        {
            linkedObjects[i].GetComponent<DoorBehaviour>().Open();
        }
    }

    public void Deactivation()
    {
        /* foreach (GameObject activables in linkedObjects)
         {
             if (activables.GetComponent<DoorBehaviour>() != null)
             {
                 activables.GetComponent<DoorBehaviour>().Close();
             }
         }*/
        for (int i = 0; i < linkedObjects.Length; i++)
        {
            linkedObjects[i].GetComponent<DoorBehaviour>().Close();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerOneInteractions>() != null)
        {
            other.gameObject.GetComponent<PlayerOneInteractions>().isInCraftRange = true;
            other.gameObject.GetComponent<PlayerOneInteractions>().nearCraftObject = this.gameObject;
        }
        if (other.gameObject.GetComponent<PlayerTwoInteractions>() != null)
        {
            other.gameObject.GetComponent<PlayerTwoInteractions>().isInCraftRange = true;
            other.gameObject.GetComponent<PlayerTwoInteractions>().nearCraftObject = this.gameObject;

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerOneInteractions>() != null)
        {
            other.gameObject.GetComponent<PlayerOneInteractions>().isInCraftRange = false;
            other.gameObject.GetComponent<PlayerOneInteractions>().nearCraftObject = null;
        }
        if (other.gameObject.GetComponent<PlayerTwoInteractions>() != null)
        {
            other.gameObject.GetComponent<PlayerTwoInteractions>().isInCraftRange = false;
            other.gameObject.GetComponent<PlayerTwoInteractions>().nearCraftObject = null;
        }
    }
}
