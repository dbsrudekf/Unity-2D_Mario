using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushRoomMonster : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;
    BoxCollider2D col;

    public GameObject ScoreUIPos;
    GameObject canvas;
    public GameObject prefabScoreUI;
    GameObject ScoreUI;
    float fScoreUIxPos = 70.0f;
    float fScoreUILimitTime = 0.5f;
    bool bIsScoreUI = false;

    public int nextMove;
    float fTime = 0.0f;
    float fLimitTime = 0.5f;
    bool bIsDeath = false;

    AudioSource audioSource;
    public AudioClip audioMonsterDead;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        col = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();
        Invoke("MovePattern", 5);
        canvas = GameObject.Find("Canvas");
    }

    private void FixedUpdate()
    {
        
        
        if (!anim.GetBool("IsDeath"))
        {
            rigid.velocity = new Vector2(nextMove, rigid.velocity.y);
        }
        
        if(anim.GetBool("IsDeath") || bIsDeath)
        {
            fTime += Time.deltaTime;

            if(!bIsScoreUI)
            {
                ScoreUI = Instantiate(prefabScoreUI, canvas.transform);

                Vector2 _ScoreUIPos = Camera.main.WorldToScreenPoint(ScoreUIPos.transform.position);

                _ScoreUIPos.x = _ScoreUIPos.x + fScoreUIxPos;

                ScoreUI.transform.position = _ScoreUIPos;

                bIsScoreUI = true;
            }

            if (ScoreUI)
            {
                ScoreUI.transform.Translate(new Vector2(0, 0.5f));
            }

            if (bIsDeath)
            {
                fLimitTime = 2.0f;
            }

            else
            {
                fLimitTime = 0.5f;
            }

            if(fTime > fScoreUILimitTime)
            {
                Destroy(ScoreUI);
            }
            if (fTime > fLimitTime)
            {
                Destroy(gameObject);
            }
        }

    }

    void MovePattern()
    {
        if(!bIsDeath)
        {
            nextMove = Random.Range(-1, 2);
            Invoke("MovePattern", 5);
        }

    }
    
    public void OnDamaged()
    {
        //Death 애니메이션
        anim.SetBool("IsDeath", true);

        GameManager.Instance.StageScore += 100;

        audioSource.clip = audioMonsterDead;
        audioSource.Play();
        //몇초뒤 사라짐
    }

    public void TurtleDamaged(Vector2 position)
    {
        GameManager.Instance.StageScore += 100;

        audioSource.clip = audioMonsterDead;
        audioSource.Play();

        bIsDeath = true;
        if(position.x > gameObject.transform.position.x)
        {
            nextMove = -1;
        }
        else
        {
            nextMove = 1;
        }
       

        rigid.velocity = new Vector2(nextMove, 2);
        col.isTrigger = true;
        spriteRenderer.flipY = true;

    }

}
