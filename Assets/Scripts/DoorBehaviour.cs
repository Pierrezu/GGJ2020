using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }


    public void Open()
    {
        anim.SetBool("isOpen", true);
        anim.SetBool("isClosed", false);
    }
    public void Close()
    {
        anim.SetBool("isOpen", false);
        anim.SetBool("isClosed", true);
    }
}
