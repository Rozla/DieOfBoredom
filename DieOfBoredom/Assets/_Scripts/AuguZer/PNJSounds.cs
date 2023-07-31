using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PNJSounds : MonoBehaviour
{
    [SerializeField] AudioClip[] voiceClip;

    [SerializeField] AudioSource pnjSource;


    private void Update()
    {
        if (GameManager.GameWin)
        {
            PlayVoiceClip(0, .3f);
        }
        if (GameManager.GameLost)
        {
            PlayVoiceClip(1, 1f);
        }
    }

    private void PlayVoiceClip(int voiceIndex, float volume)
    {
        AudioClip clip = VoiceClip(voiceIndex);
        if (!pnjSource.isPlaying)
        {
            pnjSource.PlayOneShot(clip, volume);
        }
    }

    private AudioClip VoiceClip(int index)
    {
        return voiceClip[index];
    }

}
