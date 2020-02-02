using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehaviour : MonoBehaviour
{
    public GameObject vfxLaser;
    public Transform anchor;
    public int nbOfPilePlugged = 0;
    public int nbOfPileRequired = 2;

    private void Shoot()
    {
        if(vfxLaser != null)
        {
            Instantiate(vfxLaser, anchor.position, Quaternion.identity);
        }
    }
    public void CheckIfCanShoot()
    {
        nbOfPilePlugged++;
        if(nbOfPilePlugged>=nbOfPileRequired)
        {
            Shoot();
        }
    }
}
