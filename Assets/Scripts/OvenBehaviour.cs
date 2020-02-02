using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvenBehaviour : MonoBehaviour
{
    public bool isActivated;

    private int QTEgen;
    private int correctKey;
    private int nbOfKeyToBePressed;

    public int maxNbOfKey;
    public int minNbofKey;
    private int actualNbOfKeyPressed;

    public GameObject[] keyImages;
    private bool isInQteMode;
    public float timeBeforeStartingQTE = 1;
    public GameObject actualPlayerUsingOven;
    public float timeBetweenInputs = 1.5f;
    private GameObject actualkey;
    private bool havePressedAButton;

    public float reactionTime = 1.1f;

    [Header("Recipe")]
    public bool pileIsInside;
    public int nbOfLingotsRequired;
    public int actualNbOfLingots;

    public GameObject failedObjectCrafted;
    public GameObject ObjectCrafted;
    public float expulsionForce = 200;
    [Header("VFX variables")]
    public GameObject startBurningVFX;
    public GameObject successInputVFX;
    public GameObject FailedInputVFX;

    public int nbOfLingots;

    private void Start()
    {
        nbOfKeyToBePressed = Random.Range(minNbofKey, maxNbOfKey);
    }
    public void StartBurning()
    {
        if (!isInQteMode)
        {
            if (startBurningVFX != null)
            {
                Instantiate(startBurningVFX, GetComponentInChildren<ParticleSystem>().transform.position, Quaternion.identity);
            }
            if (actualNbOfLingots >= nbOfLingotsRequired && pileIsInside)
            {
                Invoke("KeyGeneration", timeBeforeStartingQTE);
                actualPlayerUsingOven.GetComponent<PlayerController>().AuthorizedToMove = false;
                actualPlayerUsingOven.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }
    }

    private void KeyGeneration()
    {
        havePressedAButton = false;
        QTEgen = Random.Range(1, 5);

        if (QTEgen == 1)
        {
            correctKey = 1;
        }
        if (QTEgen == 2)
        {
            correctKey = 2;
        }
        if (QTEgen == 3)
        {
            correctKey = 3;
        }
        if (QTEgen == 4)
        {
            correctKey = 4;
        }
        actualkey = Instantiate(keyImages[correctKey - 1], actualPlayerUsingOven.transform.position + Vector3.up * 2, Quaternion.identity);
        actualkey.GetComponent<ButtonBehaviour>().playerPos = actualPlayerUsingOven.transform;
        isInQteMode = true;
        Invoke("CheckIfPlayerPressedAButton", 0.99f);
    }

    private void Update()
    {
        if (isInQteMode)
        {
            if (actualPlayerUsingOven.GetComponent<PlayerOneInteractions>() != null)
            {
                if (Input.anyKeyDown)
                {
                    if (correctKey == 1)
                    {
                        if (Input.GetButtonDown("A1"))
                        {
                            InputSuccess();
                        }
                        else
                        {
                            FailedCraft();
                        }
                    }

                    if (correctKey == 2)
                    {
                        if (Input.GetButtonDown("B1"))
                        {
                            InputSuccess();
                        }
                        else
                        {
                            FailedCraft();
                        }
                    }

                    if (correctKey == 3)
                    {
                        if (Input.GetButtonDown("X1"))
                        {
                            InputSuccess();
                        }
                        else
                        {
                            FailedCraft();
                        }
                    }

                    if (correctKey == 4)
                    {
                        if (Input.GetButtonDown("Y1"))
                        {
                            InputSuccess();
                        }
                        else
                        {
                            FailedCraft();
                        }
                    }
                    havePressedAButton = true;
                }
            }
            if (actualPlayerUsingOven.GetComponent<PlayerTwoInteractions>() != null)
            {
                if (Input.anyKeyDown)
                {
                    if (correctKey == 1)
                    {
                        if (Input.GetButtonDown("A2"))
                        {
                            InputSuccess();
                        }
                        else
                        {
                            FailedCraft();
                        }
                    }

                    if (correctKey == 2)
                    {
                        if (Input.GetButtonDown("B2"))
                        {
                            InputSuccess();
                        }
                        else
                        {
                            FailedCraft();
                        }
                    }

                    if (correctKey == 3)
                    {
                        if (Input.GetButtonDown("X2"))
                        {
                            InputSuccess();
                        }
                        else
                        {
                            FailedCraft();
                        }
                    }

                    if (correctKey == 4)
                    {
                        if (Input.GetButtonDown("Y2"))
                        {
                            InputSuccess();
                        }
                        else
                        {
                            FailedCraft();
                        }
                    }
                    havePressedAButton = true;
                }
            }
        }
    }

    private void InputSuccess()
    {
        Debug.Log("Success");
        if (successInputVFX != null)
        {
            Instantiate(successInputVFX, actualkey.transform.position, Quaternion.identity);
        }
        Destroy(actualkey.gameObject);
        actualNbOfKeyPressed++;
        if (actualNbOfKeyPressed >= nbOfKeyToBePressed)
        {
            CraftSuccess();
        }
        else
        {
            Invoke("KeyGeneration", timeBetweenInputs);
        }
    }
    private void CraftSuccess()
    {
        actualNbOfLingots = 0;
        Debug.Log("Lingot Crafté!");
        isInQteMode = false;
        pileIsInside = false;
        actualPlayerUsingOven.GetComponent<PlayerController>().AuthorizedToMove = true;
        actualNbOfKeyPressed = 0;
        GiveCraftedObject();
    }
    private void GiveCraftedObject()
    {
        // for (int i = 0; i < nbOfKeyToBePressed; i++)
        //{
        GameObject clone = Instantiate(ObjectCrafted, transform.GetChild(0).transform.position + Vector3.up * 2, Quaternion.identity);
        clone.AddComponent<Rigidbody>();
        clone.GetComponent<Collider>().isTrigger = false;
        clone.GetComponent<Rigidbody>().AddForce(Vector3.back * expulsionForce + Vector3.up * expulsionForce);
        //}
    }
    private void FailedCraft()
    {
        if (FailedInputVFX != null)
        {
            Instantiate(FailedInputVFX, actualkey.transform.position, Quaternion.identity);
        }
        pileIsInside = false;
        actualNbOfLingots = 0;
        Destroy(actualkey.gameObject);
        actualPlayerUsingOven.GetComponent<PlayerController>().AuthorizedToMove = true;
        isInQteMode = false;
        actualNbOfKeyPressed = 0;
        GameObject clone = Instantiate(failedObjectCrafted, transform.GetChild(0).transform.position + Vector3.up * 2, Quaternion.identity);
        clone.AddComponent<Rigidbody>();
        clone.GetComponent<Collider>().isTrigger = false;
        clone.GetComponent<Rigidbody>().AddForce(Vector3.back * expulsionForce + Vector3.up * expulsionForce);
        Debug.Log("fail");
    }
    private void CheckIfPlayerPressedAButton()
    {
        if (!havePressedAButton)
        {
            FailedCraft();
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
