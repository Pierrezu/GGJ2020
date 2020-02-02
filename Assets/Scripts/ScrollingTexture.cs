using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingTexture : MonoBehaviour {
    Material m_mat;
    public bool scrollMainTex;
    public bool scrollNormal;
    private float scrollXSpeed;
    private float scrollYSpeed;
    private Vector2 scrollSpeed;
    [Range(-1,1)] public float xMinSpeed, xMaxSpeed;
    [Range(-1, 1)] public float yMinSpeed, yMaxSpeed;

    private void Start()
    {
        m_mat = GetComponent<MeshRenderer>().material;
        SetSpeed();
    }
    public void SetSpeed()
    {
        scrollXSpeed = Random.Range(xMinSpeed, xMaxSpeed);
        scrollYSpeed = Random.Range(yMinSpeed, yMaxSpeed);
        scrollSpeed = new Vector2(scrollXSpeed, scrollYSpeed);
    }
    void LateUpdate ()
    {

        if (scrollMainTex)
        {
            m_mat.mainTextureOffset = scrollSpeed * Time.time;
        }
        if(scrollNormal)
        {
            m_mat.SetTextureOffset("_BumpMap", m_mat.GetTextureOffset("_BumpMap")+scrollSpeed*Time.deltaTime);
        }
        //m_mat.SetTextureOffset("_DissolveTexture", scrollSpeed * Time.time);
    }

}
