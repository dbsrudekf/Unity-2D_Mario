using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushRoomItem : MonoBehaviour
{
    float fTime = 0.0f;
    float fLimitTime = 2.0f;
    float fMaxMovePatternTime = 2.3f;
    float fMovePatternTime = 0.0f;
    public int nextMove = 0;


    Rigidbody2D rigid;
    BoxCollider2D BoxCollider;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        BoxCollider = GetComponent<BoxCollider2D>();
    }
    void Start()
    {
 
    }
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        fMovePatternTime += Time.deltaTime;
        fTime += Time.deltaTime;
        if(fTime < fLimitTime)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, 0.5f);
            
        }
        else
        {
            BoxCollider.isTrigger = false;
            rigid.velocity = new Vector2(nextMove * 2, rigid.velocity.y);
            MovePattern();
        }
    }
    public void MovePattern()
    {
        if(fMovePatternTime > fMaxMovePatternTime)
        {
            nextMove = Random.Range(-1, 2);
            if(nextMove == 0)
            {
                nextMove = 1;
            }
            fMovePatternTime = 0.0f;
        }

    }
}
