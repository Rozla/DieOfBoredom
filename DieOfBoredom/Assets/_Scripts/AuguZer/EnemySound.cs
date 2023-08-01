using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySound : MonoBehaviour
{
    [SerializeField] AudioClip[] angryVoice;
    [SerializeField] AudioClip[] sadVoice;

    [SerializeField] AudioSource teacherSource;
    private void Voice1()
    {
        AudioClip clip = RandomAngryVoice();
        teacherSource.PlayOneShot(clip);

    }

    private void Voice2()
    {
        AudioClip clip = RandomSadVoice();
        teacherSource.PlayOneShot(clip);
    }

    private void Voice3()
    {
        
    }
    private AudioClip RandomAngryVoice()
    {
        return angryVoice[Random.Range(0,angryVoice.Length)];
    }
    private AudioClip RandomSadVoice()
    {
        return sadVoice[Random.Range(0,sadVoice.Length)];
    }
   
}
