using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;


public class AmbientAudioSystem : MonoBehaviour
{
    public List<AudioClip> clips = new List<AudioClip>();

    public GameObject targetListener;

    public AudioSource sourceFab;

    private AudioSource current;

    public float minDistance;
    public float maxDistance;

    public float minTime;
    public float maxTime;

    public float minVolume;
    public float maxVolume;

    public float minPitch;
    public float maxPitch;

    private float timer;

    private void Update()
    {
        if(current == null)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            if(!current.isPlaying)
            {
                Destroy(current.gameObject);
                current = null;
            }
        }

        if(timer < 0)
        {
            FireAudio();
        }
    } 

    private void FireAudio()
    {
        current = Instantiate(sourceFab, targetListener.transform.position, Quaternion.identity, this.transform);
        timer = Random.Range(minTime, maxTime);
        current.pitch = Random.Range(minPitch, maxPitch);
        current.volume = Random.Range(minVolume, maxVolume);
        current.clip = clips[Random.Range(0, clips.Count)];

        float dist = Random.Range(minDistance, maxDistance);
        float angle = Random.Range(0f, 360f);

        current.transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
        current.transform.position += current.transform.forward * dist;
        transform.position += Vector3.up * Random.Range(0f, 3f);

        current.Play();

    }
}