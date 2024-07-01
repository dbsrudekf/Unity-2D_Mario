using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWall : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip audioItem;

    public GameObject[] prefabs;

    Animator anim;

    bool bItemOn = true;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && bItemOn && transform.position.y > collision.transform.position.y)
        {
            audioSource.clip = audioItem;

            audioSource.Play();

            bItemOn = false;
            
            anim.SetBool("IsItemOff", true);

            int iselection = Random.Range(0, prefabs.Length);

            GameObject selectedPrefab = prefabs[iselection];

            Vector3 spawnPos = transform.position;

            spawnPos.z = -1;

            GameObject instance = Instantiate(selectedPrefab, spawnPos, Quaternion.identity);

        }
    }
}
