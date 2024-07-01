using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutleMonster : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriteRenderer;
    BoxCollider2D col;

    public GameObject ScoreUIPos;
    GameObject canvas;
    public GameObject prefabScoreUI;
    GameObject ScoreUI;
    float fScoreUIxPos = 70.0f;
    float fScoreUILimitTime = 0.5f;
    bool bIsScoreUI = false;

    public int nextMove;
    public int ColliderCount = 0;
    float fTime = 0.0f;
    float fLimitTime = 2.0f;

    bool bIsDeath = false;
    bool bIsDoubleDeath = false;

    AudioSource audioSource;
    public AudioClip audioMonsterDead;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();
        Invoke("MovePattern", 3);
        canvas = GameObject.Find("Canvas");
    }

    private void FixedUpdate()
    {
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        if(bIsDeath)
        {
            fTime += Time.deltaTime;

            if (!bIsScoreUI)
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

            if (fTime > fScoreUILimitTime)
            {
                Destroy(ScoreUI);
                fTime = 0;
            }
        }

        if(bIsDoubleDeath)
        {
            fTime += Time.deltaTime;

            if (!bIsScoreUI)
            {
                ScoreUI = Instantiate(prefabScoreUI, canvas.transform);

                Vector2 _ScoreUIPos = Camera.main.WorldToScreenPoint(ScoreUIPos.transform.position);

                _ScoreUIPos.x = _ScoreUIPos.x + fScoreUIxPos;

                ScoreUI.transform.position = _ScoreUIPos;

                bIsScoreUI = true;

                Debug.Log("ScoreUI");
            }

            if (ScoreUI)
            {
                ScoreUI.transform.Translate(new Vector2(0, 0.5f));
            }

            if (fTime > fScoreUILimitTime)
            {
                Destroy(ScoreUI);

            }

            if (fTime > fLimitTime)
            {
                Destroy(gameObject);
                
                bIsDoubleDeath = false;
            }

            
        }
    }
    void MovePattern()
    {
        if(!anim.GetBool("IsDeath"))
        {
            int RandomMove = Random.Range(0, 2);
            switch (RandomMove)
            {
                case 0:
                    nextMove = 1;
                    break;
                case 1:
                    nextMove = -1;
                    break;
            }

            Invoke("MovePattern", 3);
            spriteRenderer.flipX = nextMove == -1; //nextMove가 -1일 때 flipX가 활성화
        }
    }

    void OnAttack()
    {
        if(anim.GetBool("IsAttack"))
        {
            nextMove = 4;
        }
        else
        {
            nextMove = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            nextMove = nextMove * -1;
            spriteRenderer.flipX = nextMove == -1; //nextMove가 -1일 때 flipX가 활성화
        }

        if(bIsDeath && anim.GetBool("IsAttack"))
        {
            if (collision.gameObject.tag == "MonsterTurtle")
            {
                //날아간다.
                TutleMonster turtleMonster = collision.gameObject.GetComponent<TutleMonster>();
                turtleMonster.TurtleDamaged(gameObject.transform.position);

            }

            if(collision.gameObject.tag == "MonsterMushroom")
            {
                //날아간다.
                MushRoomMonster mushroomMonster = collision.gameObject.GetComponent<MushRoomMonster>();
                mushroomMonster.TurtleDamaged(gameObject.transform.position);

            }
        }
        
    }

    public void OnDamaged()
    {
        audioSource.clip = audioMonsterDead;
        audioSource.Play();

        if (!bIsDeath)
        {
            GameManager.Instance.StageScore += 200;
        }
        

        gameObject.layer = 12;
        anim.SetBool("IsDeath", true);
        col.size = new Vector2(0.17f, 0.16f);
        nextMove = 0;
        ColliderCount++;
        bIsDeath = true;

        

        if (ColliderCount != 0 && ColliderCount % 2 == 0)
        {
            anim.SetBool("IsAttack", true);
            OnAttack();
        }
        else
        {
            anim.SetBool("IsAttack", false);
        }
    }

    public void TurtleDamaged(Vector2 position)
    {
        GameManager.Instance.StageScore += 200;

        audioSource.clip = audioMonsterDead;
        audioSource.Play();

        bIsDoubleDeath = true;
        if (position.x > gameObject.transform.position.x)
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
