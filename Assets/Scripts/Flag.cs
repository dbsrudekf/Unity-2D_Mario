using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    bool bIsCol = false;
    bool bIsAnim = false;
    float fLimitFlagPos = -2.6f;
    GameObject player;
    Vector3 pos;

    private void Awake()
    {
        pos = transform.position;
    }
    private void Update()
    {
        player = GameObject.Find("Mario");

        if (bIsCol)
        {
            pos.y -= 1 * Time.deltaTime * 2;
            pos.z = -1.4f;
            transform.position = pos;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (transform.position.y < fLimitFlagPos)
            {
                bIsCol = false;
                player.GetComponent<SpriteRenderer>().flipX = true;
                player.GetComponent<Transform>().position = new Vector3(60.4f, collision.transform.position.y, transform.position.z);
                player.GetComponent<Animator>().SetBool("IsFlag", false);
                player.GetComponent<Animator>().SetBool("IsFlagFinish", true);
            }
            else
            {
                bIsCol = true;
                player.GetComponent<Animator>().SetBool("IsFlag", true);

            }
        }
    }

}
