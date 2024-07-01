using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    float fSpeed = 5.0f;
    float fFireBulletDir = 0.0f;
    float fCurTime = 0.0f;
    float fLimitTime = 10.0f;

    Rigidbody2D rigid;

    bool bFlipX = false;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        GameObject player = GameObject.Find("Mario");
        bFlipX = player.GetComponent<SpriteRenderer>().flipX;
        
        if(bFlipX)
        {
            fFireBulletDir = -1.0f;
        }
        else
        {
            fFireBulletDir = 1.0f;
        }
    }
    private void FixedUpdate()
    {

        rigid.velocity = new Vector2(fFireBulletDir * fSpeed, rigid.velocity.y);

    }

    private void Update()
    {
        fCurTime += Time.deltaTime;
        
        if(fCurTime > fLimitTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MonsterMushroom")
        {
            MushRoomMonster mushroomMonster = collision.gameObject.GetComponent<MushRoomMonster>();
            mushroomMonster.TurtleDamaged(gameObject.transform.position);
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "MonsterTurtle")
        {
            TutleMonster TurtleMonster = collision.gameObject.GetComponent<TutleMonster>();
            TurtleMonster.TurtleDamaged(gameObject.transform.position);
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Obstacle")
        {
            Destroy(gameObject);
        }
    }
}
