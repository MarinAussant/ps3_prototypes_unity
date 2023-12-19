using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlacement : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip soundPlacement1;
    public AudioClip soundPlacement2;
    public AudioClip soundPlacement3;



    // Start is called before the first frame update
    void Start()
    {
        audioSource.volume = 0.3f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayRandomSound()
    {
        AudioClip[] soundList = { soundPlacement1, soundPlacement2, soundPlacement3};

        audioSource.clip = soundList[Random.Range(0, 2)];
        audioSource.Play();
        Debug.Log("son placement");

    }
}
