using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioClip pnjWinVoice;
    [SerializeField] AudioClip pnjLooseVoice;
    [SerializeField] AudioClip jingleWin;
    [SerializeField] AudioClip jingleLoose;
    [SerializeField] AudioClip schoolBell;
    [SerializeField] AudioClip inGameMusicClip;

    [SerializeField] AudioSource pnjSource;
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource jingleSource;
    [SerializeField] AudioSource backgroundSource;
    [SerializeField] AudioSource ringSource;

    [SerializeField] BoxBehaviour boxBehaviour;
    [SerializeField] LoseTimer loseTimer;

    private void Start()
    {
        backgroundSource.Play();
        musicSource.Play();

        boxBehaviour._winEvent.AddListener(() => {
            StartCoroutine(WinSoundCoroutine());
        });

        loseTimer._loseEvent.AddListener(() =>
        {
            StartCoroutine(LooseSoundCoroutine());
        });
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

    IEnumerator WinSoundCoroutine()
    {
        yield return new WaitForSeconds(2.6f);
        ringSource.PlayOneShot(schoolBell);
        yield return new WaitForSeconds(1f);
        pnjSource.PlayOneShot(pnjWinVoice);
        jingleSource.PlayOneShot(jingleWin);

    } 
    IEnumerator LooseSoundCoroutine()
    {
        yield return new WaitForSeconds(.2f);
        pnjSource.PlayOneShot(pnjLooseVoice);
        jingleSource.PlayOneShot(jingleLoose);

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
