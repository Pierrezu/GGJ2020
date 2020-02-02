using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ButtonBehaviour : MonoBehaviour
{
    public GameObject apparitionVfx;
    public Transform playerPos;
    public float heightFromPlayer=2;
    private Vector3 initialScale;
    private void Start()
    {
        initialScale = transform.localScale;
        // transform.DOScale(3, 0.1f).SetLoops(-1, LoopType.Yoyo);
        transform.localScale = Vector3.zero;
        transform.DOScale(initialScale, 0.3f);
    }

    private void Update()
    {
        transform.position = playerPos.position + Vector3.up * heightFromPlayer;
    }
}
