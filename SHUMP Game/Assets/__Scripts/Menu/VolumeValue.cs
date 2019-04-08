using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeValue : MonoBehaviour
{
    private AudioSource _audioSrc;
    public static float VOLUME = 1f;
    //Get the audio source component playing background music
    void Start()
    {
        _audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        _audioSrc.volume = VOLUME; //Updates audio source volume to that set by the slider in volume set method
    }
    public void volumeSet(float volume)
    {
        VOLUME = volume; //Set volume value 
    }
}
