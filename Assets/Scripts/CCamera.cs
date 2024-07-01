using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCamera : MonoBehaviour
{

    GameObject Player;
    static public CCamera instance;

    public bool bIsBossStage;
    private void Awake()
    {
        if(instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        bIsBossStage = false;
    }

    void Start()
    {
        Player = GameObject.Find("Mario");
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    void Update()
    {
        Vector3 PlayerPos = Player.transform.position;

        if (bIsBossStage)
        {
            transform.position = new Vector3(PlayerPos.x, 0.8f , transform.position.z);
        }
        else
        {
            transform.position = new Vector3(PlayerPos.x, transform.position.y, transform.position.z);
        }
        
    }
}
