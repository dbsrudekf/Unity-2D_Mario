using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBridge : MonoBehaviour
{
    public GameObject BossStair;
    public GameObject BossHammer;
    public GameObject BossMonster;
    public GameObject BossBullet;
    BoxCollider2D col;
    float fCurrentTime = 0.0f;
    float fLimitTime = 0.1f;

    int iCount = 12;
    public bool bCol;

    private void Awake()
    {
        col = GetComponent<BoxCollider2D>();
        bCol = false;
    }

    void Update()
    {
        fCurrentTime += Time.deltaTime;
        
        if (bCol && fCurrentTime > fLimitTime)
        {
            BridgeDestroy();
            fCurrentTime = 0.0f;
        }
    }

    void BridgeDestroy()
    {
        transform.GetChild(iCount).gameObject.SetActive(false);

        iCount--;
        Destroy(BossStair);

        if (iCount == 0)
        {
            Destroy(gameObject);
            Destroy(BossHammer);
            BossMonster.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
    }
}
