using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnaceBehaviour : MonoBehaviour
{
    public GameObject linkedObject;
    public float timeLeft = 30.0f;
    private bool isWorking = true;
    private float initialTime;
    public GameObject startBurningVFX;
    private ParticleSystem ps;
    private AudioSource audiosource;

    public bool disableAtStart;
    private void Start()
    {
        initialTime = timeLeft;
        ps = GetComponentInChildren<ParticleSystem>();
        audiosource = GetComponent<AudioSource>();
        if (!disableAtStart)
        {
            ResetCoolDown();
        }
        else
        {
            timeLeft = 0f;
        }
        
    }
    void Update()
    {
        if (isWorking)
        {
            CountDown();
        }
    }

    private void CountDown()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            StopWorking();
        }
    }
    public void ResetCoolDown()
    {
        audiosource.Play();
        if (linkedObject.GetComponentInChildren<ScrollingTexture>() != null)
        {
            linkedObject.GetComponentInChildren<ScrollingTexture>().enabled = true;
        }
        ps.Play();
        Instantiate(startBurningVFX, GetComponentInChildren<ParticleSystem>().transform.position, Quaternion.identity);
        isWorking = true;
        timeLeft = initialTime;
        if (linkedObject.GetComponent<OvenBehaviour>() != null)
        {
            linkedObject.GetComponent<OvenBehaviour>().isActivated = true;
            if (linkedObject.GetComponentInChildren<ParticleSystem>() != null)
            {
                linkedObject.GetComponentInChildren<ParticleSystem>().Play();
            }
            if(linkedObject.GetComponent<AudioSource>()!= null)
            {
                linkedObject.GetComponent<AudioSource>().Play();
            }
        }
        if (linkedObject.GetComponent<FabricsBehaviour>() != null)
        {
            if (linkedObject.GetComponent<FabricsBehaviour>().isActivated == false)
            {
                linkedObject.GetComponent<FabricsBehaviour>().StartCreating();
            }
            linkedObject.GetComponent<FabricsBehaviour>().isActivated = true;
        }
    }
    private void StopWorking()
    {
        audiosource.Stop();
        if (linkedObject.GetComponentInChildren<ScrollingTexture>() != null)
        {
            linkedObject.GetComponentInChildren<ScrollingTexture>().enabled = false;
        }
        ps.Stop();
        isWorking = false;
        if(linkedObject.GetComponent<OvenBehaviour>() != null)
        {
            linkedObject.GetComponent<OvenBehaviour>().isActivated = false;
            if(linkedObject.GetComponentInChildren<ParticleSystem>()!= null)
            {
                linkedObject.GetComponentInChildren<ParticleSystem>().Stop();
            }
            if (linkedObject.GetComponent<AudioSource>() != null)
            {
                linkedObject.GetComponent<AudioSource>().Stop();
            }
        }
        if (linkedObject.GetComponent<FabricsBehaviour>() != null)
        {
            linkedObject.GetComponent<FabricsBehaviour>().isActivated = false;
            linkedObject.GetComponent<FabricsBehaviour>().CancelInvoke();
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
