using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public AudioClip audioCoin;
    AudioSource audioSource;

    GameObject ScoreUIPos;
    public GameObject prefabScoreUI;
    GameObject canvas;
    GameObject ScoreUI;
    float fScoreUIxPos = 70.0f;
    float fScoreUILimitTime = 0.5f;
    bool bIsScoreUI = false;

    float fLimitTime = 0.5f;
    float fCurrentTime = 0.0f;
    void Start()
    {
        GameManager.Instance.StageScore += 100;
        GameManager.Instance.StageCoin += 1;
        canvas = GameObject.Find("Canvas");
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioCoin;
        audioSource.Play();
    }

    private void FixedUpdate()
    {
        if (!bIsScoreUI)
        {
            ScoreUI = Instantiate(prefabScoreUI, canvas.transform);

            Vector2 _ScoreUIPos = Camera.main.WorldToScreenPoint(transform.position);

            _ScoreUIPos.x = _ScoreUIPos.x + fScoreUIxPos;

            ScoreUI.transform.position = _ScoreUIPos;

            bIsScoreUI = true;
        }

        if (ScoreUI)
        {
           
            ScoreUI.transform.Translate(new Vector2(0, 0.5f));
        }
    }

    private void Update()
    {
        fCurrentTime += Time.deltaTime;

        transform.Translate(Vector2.up * Time.deltaTime * 5.0f);

        if(fLimitTime < fCurrentTime)
        {
            Destroy(gameObject);
            Destroy(ScoreUI);
        }
    }
}
