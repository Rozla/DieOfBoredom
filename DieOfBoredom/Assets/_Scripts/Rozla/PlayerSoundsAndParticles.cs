using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundsAndParticles : MonoBehaviour
{
    [SerializeField] AudioClip[] _stepClips;

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
        _audioSource.PlayOneShot(clip);
        _audioSource.pitch = Random.Range(.9f,1.1f);
        _audioSource.volume = Random.Range(.1f, .3f);
    }

    private AudioClip RandomStepClip()
    {
        return _stepClips[Random.Range(0, _stepClips.Length)];
    }
}
