using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FabricsBehaviour : MonoBehaviour
{
    public GameObject createdObject;
    public bool isActivated;
    public float creatingRate = 3;
    public Transform spawningPoint;
    public Vector3 direction;
    public float speed;
    public float timeBeforeDestroy = 60f;
    private void Start()
    {
        StartCreating();
    }
    public void StartCreating()
    {
        InvokeRepeating("Create", 0f, creatingRate);
    }
    private void Create()
    {
        if (createdObject != null)
        {
            GameObject clone = Instantiate(createdObject, spawningPoint.position, Quaternion.identity);
            clone.AddComponent<Rigidbody>();
            clone.GetComponent<Rigidbody>().useGravity = false;
            clone.GetComponent<Rigidbody>().velocity = direction.normalized * speed;
            Destroy(clone, timeBeforeDestroy);
        }

    }
}
