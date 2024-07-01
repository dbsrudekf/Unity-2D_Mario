using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int TotalScore = 0;
    public int StageScore = 0;
    public int TotalCoin = 0;
    public int StageCoin = 0;
    public int StageIndex = 1;
    public int SubStageIndex = 1;
    public float TimeLimit = 400;

    public Text UIScore;
    public Text UICoin;
    public Text UIStage;
    public Text UITime;
    public Text BossStageMsg;
    public Text BossStageNextMsg;

    public AudioSource audioSource;
    public AudioClip audioStageOne;
    public AudioClip audioCuppaStage;


    private static GameManager instance = null;

    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if(instance)
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        StageIndex = 1;
        SubStageIndex = 1;
        BossStageNextMsg.text = "BUT OUR PRINCESS IS IN ANOTHER CASTLE!";
        BossStageMsg.text = "THANK YOU MARIO!";
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        
        BossStageMsg.enabled = false;
        BossStageNextMsg.enabled = false;
        audioSource.clip = audioStageOne;
        audioSource.Play();


    }
    private void Update()
    {
        UIScore.text = (TotalScore + StageScore).ToString();
        UICoin.text = (TotalCoin + StageCoin).ToString();
        TimeLimit -= Time.deltaTime;
        UITime.text = ((int)TimeLimit).ToString();

        UIStage.text = StageIndex + " - " + SubStageIndex;
        if(SubStageIndex > 4)
        {
            StageIndex++;
            SubStageIndex = 1;
        }
    }
}
