using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvenBehaviour : MonoBehaviour
{
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

    public float reactionTime =1.1f;

    [Header("VFX variables")]
    public GameObject startBurningVFX;

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
                Instantiate(startBurningVFX, transform.position, Quaternion.identity);
            }
            Invoke("KeyGeneration", timeBeforeStartingQTE);
        }
    }

    private void KeyGeneration()
    {
        havePressedAButton = false;
        QTEgen = Random.Range(1, 4);

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
       actualkey = Instantiate(keyImages[correctKey - 1], transform.position + Vector3.up * 2, Quaternion.identity);

        isInQteMode = true;
        Invoke("CheckIfPlayerPressedAButton",0.75f);
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
        Debug.Log("Lingot Crafté!");
        isInQteMode = false;
        actualPlayerUsingOven.GetComponent<PlayerController>().AuthorizedToMove = true;
        actualNbOfKeyPressed = 0;
    }
    private void FailedCraft()
    {
        Destroy(actualkey.gameObject);
        actualPlayerUsingOven.GetComponent<PlayerController>().AuthorizedToMove = true;
        isInQteMode = false;
        actualNbOfKeyPressed = 0;
        Debug.Log("fail");
    }
    private void CheckIfPlayerPressedAButton()
    {
        if(!havePressedAButton)
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
