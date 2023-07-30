using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundsAndParticles : MonoBehaviour
{
    [SerializeField] AudioClip[] _stepClips;
    [SerializeField] AudioClip[] _crouchStepClips;

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
        _audioSource.volume = Random.Range(.05f, .1f);
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
}
