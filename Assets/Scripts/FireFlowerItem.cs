using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFlowerItem : MonoBehaviour
{
    float fTime = 0.0f;
    float fLimitTime = 2.0f;

    Rigidbody2D rigid;
    BoxCollider2D BoxCollider;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        BoxCollider = GetComponent<BoxCollider2D>();
    }

    void FixedUpdate()
    {
        fTime += Time.deltaTime;
        if (fTime < fLimitTime)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, 0.5f);

        }
        else
        {
            BoxCollider.isTrigger = false;
        }
    }
}
