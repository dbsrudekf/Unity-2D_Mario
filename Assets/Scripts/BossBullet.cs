using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public bool bIsHammer;
    public float fBulletDir = -1.0f;
    bool bFlipX = false;
    float fSpeed = 2.0f;

    GameObject GBossMonster;

    private void Awake()
    {
        bIsHammer = false;
        GBossMonster = GameObject.Find("TurtleBossMonster");
        bFlipX = GBossMonster.GetComponent<SpriteRenderer>().flipX;
       
        if (bFlipX)
        {
            fBulletDir = 1.0f;
        }
        else
        {
            fBulletDir = -1.0f;
        }
    }
    void FixedUpdate()
    {
        if (GBossMonster)
        {
            if (GBossMonster.GetComponent<Rigidbody2D>().bodyType == RigidbodyType2D.Static)
            {
                bIsHammer = true;
            }
            else
            {
                bIsHammer = false;
            }
        }
        else
        {
            Destroy(gameObject);
        }

        if(bIsHammer)
        {
            fSpeed = 0.0f;
        }
        else
        {
            fSpeed = 2.0f;
        }

        Vector2 dir = new Vector2(fBulletDir, 0);
        transform.Translate(dir * Time.deltaTime * 2.0f * fSpeed);

        if(transform.position.x < -10 || transform.position.x > 37.7f)
        {
            Destroy(gameObject);
        }
    }
}
