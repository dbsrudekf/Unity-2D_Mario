using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinWall : MonoBehaviour
{
    public GameObject prefabs;
    public Sprite spriteImage;
    SpriteRenderer sprite;
    
    int iCount = 0;


    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if(iCount == 10)
        {
            sprite.sprite = spriteImage;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && transform.position.y > collision.transform.position.y)
        {
            if(iCount < 10)
            {
                Vector3 spawnPos = transform.position;
                spawnPos.x = spawnPos.x + 0.05f;
                spawnPos.z = -2;
                GameObject instance = Instantiate(prefabs, spawnPos, Quaternion.identity);
                iCount++;
            }
        }
    }
}
