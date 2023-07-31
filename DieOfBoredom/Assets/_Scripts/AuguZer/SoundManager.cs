using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //[SerializeField] AudioClip[] pnjVoiceClip;
    //[SerializeField] AudioClip[] jingleClip;
    [SerializeField] AudioClip inGameMusicClip;

    //[SerializeField] AudioSource pnjSource;
    [SerializeField] AudioSource musicSource;
    //[SerializeField] AudioSource jingleSource;
    [SerializeField] AudioSource backgroundSource;

    private void Start()
    {
        backgroundSource.Play();
        musicSource.Play();
    }

    private void Update()
    {
        if (GameManager.GameWin)
        {
            backgroundSource.Pause();
            musicSource.Pause();
            //PlayVoiceClip(0, .3f);
            //PlayJingleClip(0, 1f);
        }
        if (GameManager.GameLost)
        {
            backgroundSource.Pause();
            musicSource.Pause();
            //PlayVoiceClip(1, 1f);
            //PlayJingleClip(1, .5f);
        }
    }

    //private void PlayVoiceClip(int voiceIndex, float volume)
    //{
    //    AudioClip clip = VoiceClip(voiceIndex);
    //    StartCoroutine(StopLoop(clip, volume));
    //}
    //private void PlayJingleClip(int voiceIndex, float volume)
    //{
    //    AudioClip clip = JingleClip(voiceIndex);
    //    if (!jingleSource.isPlaying)
    //    {
    //        jingleSource.PlayOneShot(clip, volume);
    //    }
    //}

    //private AudioClip VoiceClip(int index)
    //{
    //    return pnjVoiceClip[index];
    //}
    //private AudioClip JingleClip(int index)
    //{
    //    return jingleClip[index];
    //}

    //IEnumerator StopLoop(AudioClip clip, float volume)
    //{
    //    if (!pnjSource.isPlaying)
    //    {
    //        pnjSource.PlayOneShot(clip, volume);
    //    }
    //    yield return new WaitForSeconds(clip.length);
    //    pnjSource.mute = true;
    //}
}
