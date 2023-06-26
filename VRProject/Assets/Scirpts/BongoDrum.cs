using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BongoDrum : MonoBehaviour
{
    private AudioSource _source;

    // Start is called before the first frame update
    void Start()
    {
        _source = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        print("collsionDetected");
        _source.Play();
    }

}
