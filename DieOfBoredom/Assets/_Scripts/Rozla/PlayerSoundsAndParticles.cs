using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundsAndParticles : MonoBehaviour
{
    [SerializeField] AudioClip[] _stepClips;
    [SerializeField] AudioClip[] _crouchStepClips;
    [SerializeField] AudioClip[] _chairClips;
    [SerializeField] AudioClip[] _gearClips;

    AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Step()
    {
        AudioClip clip = RandomStepClip();
        _audioSource.pitch = Random.Range(.7f, 1.3f);
        _audioSource.volume = Random.Range(.1f, .3f);
        _audioSource.PlayOneShot(clip);
    }

    private void CrouchStep()
    {
        AudioClip clip = RandomCrouchStepClip();
        _audioSource.pitch = Random.Range(.7f, 1.3f);
        _audioSource.volume = Random.Range(.1f, .2f);
        _audioSource.PlayOneShot(clip);
    }

    private void ChairSound()
    {
        AudioClip clip = RandomChairClip();
        _audioSource.pitch = Random.Range(.9f, 1.1f);
        _audioSource.volume = Random.Range(.3f, .5f);
        _audioSource.PlayOneShot(clip);
    }

    private void GearSound()
    {
        AudioClip clip = RandomGearClip();
        _audioSource.pitch = Random.Range(.9f, 1.1f);
        _audioSource.volume = Random.Range(.3f, .5f);
        _audioSource.PlayOneShot(clip);
    }

    private AudioClip RandomStepClip()
    {
        return _stepClips[Random.Range(0, _stepClips.Length)];
    }

    private AudioClip RandomCrouchStepClip()
    {
        return _crouchStepClips[Random.Range(0, _crouchStepClips.Length)];
    }

    private AudioClip RandomChairClip()
    {
        return _chairClips[Random.Range(0, _chairClips.Length)];
    }

    private AudioClip RandomGearClip()
    {
        return _gearClips[Random.Range(0, _gearClips.Length)];
    }
}
