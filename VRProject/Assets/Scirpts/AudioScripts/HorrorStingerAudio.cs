using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorrorStingerAudio : MonoBehaviour
{
    public List<AudioClip> clips = new List<AudioClip>();

    public AudioSource source;

    public bool active;

    public float minTime;
    public float maxTime;

    public float minVolume;
    public float maxVolume;

    public float minPitch;
    public float maxPitch;

    private float timer;

    // Update is called once per frame
    void Update()
    {
        if(!active)
        {
            return;
        }

        if(!source.isPlaying)
        {
            timer -= Time.deltaTime;
        }

        if(timer < 0)
        {
            FireAudio();
        }
    }

    private void FireAudio()
    {
        timer = Random.Range(minTime, maxTime);
        source.clip = clips[Random.Range(0, clips.Count)];
        source.pitch = Random.Range(minPitch, maxPitch);
        source.volume = Random.Range(minVolume, maxVolume);

        source.Play();
    }

    
}
