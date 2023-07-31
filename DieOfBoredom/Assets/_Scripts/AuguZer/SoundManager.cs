using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioClip[] pnjVoiceClip;
    [SerializeField] AudioClip[] jingleClip;

    [SerializeField] AudioSource pnjSource;
    [SerializeField] AudioSource musicSource;



    private void Update()
    {
        if (GameManager.GameWin)
        {
            PlayVoiceClip(0, .3f);
            PlayJingleClip(0, 1f);
        }
        if (GameManager.GameLost)
        {
            PlayVoiceClip(1, 1f);
            PlayJingleClip(1, .5f);
        }

        Debug.Log(GameManager.GameLost);
    }

    private void PlayVoiceClip(int voiceIndex, float volume)
    {
        AudioClip clip = VoiceClip(voiceIndex);
        if (!pnjSource.isPlaying)
        {
            pnjSource.PlayOneShot(clip, volume);
        }
    }
    private void PlayJingleClip(int voiceIndex, float volume)
    {
        AudioClip clip = JingleClip(voiceIndex);
        if (!musicSource.isPlaying)
        {
            musicSource.PlayOneShot(clip, volume);
        }
    }

    private AudioClip VoiceClip(int index)
    {
        return pnjVoiceClip[index];
    }
    private AudioClip JingleClip(int index)
    {
        return jingleClip[index];
    }
}
