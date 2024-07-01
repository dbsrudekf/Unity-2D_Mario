using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MushroomNPC : MonoBehaviour
{
    public float fTime;
    public float fLimitTime;
    bool bIsCol;

    private void Awake()
    {
        fTime = 0.0f;
        fLimitTime = 1.0f;
        bIsCol = false;

    }

    private void FixedUpdate()
    {
        if(bIsCol)
        {
            fTime += Time.deltaTime;
            GameManager.Instance.BossStageMsg.enabled = true;
        }

        if (fTime > fLimitTime)
        {
            GameManager.Instance.BossStageNextMsg.enabled = true;
            bIsCol = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {  
            
            bIsCol = true;
        }
    }
}
