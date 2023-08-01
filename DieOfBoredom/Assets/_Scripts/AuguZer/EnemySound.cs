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
        if (clip == null) return;
        teacherSource.PlayOneShot(clip);

    }

    private void Voice2()
    {
        AudioClip clip = RandomSadVoice();
        if (clip == null) return;
        teacherSource.PlayOneShot(clip);
    }

    private void Voice3()
    {
        AudioClip clip = teacherSource.clip;
        if (clip == null) return;
        
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
