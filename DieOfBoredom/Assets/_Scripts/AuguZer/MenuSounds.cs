using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSounds : MonoBehaviour
{
    [SerializeField] AudioClip voice1;
    [SerializeField] AudioClip voice2;
    [SerializeField] AudioClip voice3;
    [SerializeField] private AudioSource voiceSource;
 
    private void Voice1()
    {
        voiceSource.PlayOneShot(voice1);
    }

    private void Voice2()
    {
        voiceSource.PlayOneShot(voice2);
    }
    private void Voice3()
    {
        voiceSource.PlayOneShot(voice3);
    }
}
