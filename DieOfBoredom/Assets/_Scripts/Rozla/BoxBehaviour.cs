using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxBehaviour : MonoBehaviour
{
    [SerializeField] AudioClip[] _boxClipsWrong;
    [SerializeField] AudioClip[] _boxClipsGood;
    [SerializeField] AudioClip winPnjVoice;
    [SerializeField] AudioClip winJingle;


    AudioSource _boxAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        _boxAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CheckGearLeft()
    {
        if (GearsManager._allGears.Count > 0)
        {
            PlayWrongSound();
            Debug.Log("Il reste des engrenages");
        }

        if (GearsManager._allGears.Count == 0)
        {
            PlayGoodSound();
            GameManager.GameWin = true;
        }
    }

    void PlayGoodSound()
    {
        AudioClip clip = RandomBoxClipGood();
        _boxAudioSource.pitch = Random.Range(.9f, 1.1f);
        _boxAudioSource.volume = Random.Range(.1f, .3f);
        StartCoroutine(WinSoundCoroutine(clip));

    }

    private AudioClip RandomBoxClipGood()
    {
        return _boxClipsGood[Random.Range(0, _boxClipsGood.Length)];
    }

    void PlayWrongSound()
    {
        AudioClip clip = RandomBoxClipWrong();
        _boxAudioSource.pitch = Random.Range(.9f, 1.1f);
        _boxAudioSource.volume = Random.Range(.1f, .3f);
        _boxAudioSource.PlayOneShot(clip);

    }

    private AudioClip RandomBoxClipWrong()
    {
        return _boxClipsWrong[Random.Range(0, _boxClipsWrong.Length)];
    }

    IEnumerator WinSoundCoroutine(AudioClip clip)
    {
        _boxAudioSource.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        _boxAudioSource.PlayOneShot(winPnjVoice);
        _boxAudioSource.PlayOneShot(winJingle);
    }  
}
