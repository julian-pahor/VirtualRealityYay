using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drum : MonoBehaviour
{
    public AudioClip standard;
    public AudioClip tidyClip;
    public AudioClip tidyClip2;

    private AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayDrum()
    {
        int r = Random.Range(0, 1001);

        if(r == 0)
        {
            source.clip = tidyClip;
        }
        else if(r == 1)
        {
            source.clip = tidyClip2;
        }
        else
        {
            source.clip = standard;
        }
        source.Play();
    }
}
