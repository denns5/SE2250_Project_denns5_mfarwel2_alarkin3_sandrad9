using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeValue : MonoBehaviour
{
    private AudioSource _audioSrc;
    public static float VOLUME = 1f;
    void Start()
    {
        _audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        _audioSrc.volume = VOLUME; 
    }
    public void volumeSet(float volume)
    {
        VOLUME = volume;
    }
}
