using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Saucer : MonoBehaviour
{

    public float saucerTime;
    public float saucerSpeed;
    public Transform saucerSpot;
    public bool isSaucing;
    public bool abduct;
    public Light spot;
    public Transform player;

    bool isAbducting;

    public float abductSpeed;
    public float playerSpeed;

    float timer;

    public AudioClip flyClip;
    public AudioClip abductClip;

    private AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {


        if(!isSaucing)
        {
            timer += Time.deltaTime;
            if(timer > saucerTime)
            {
                source.clip = flyClip;
                source.loop = true;
                source.Play();
                isSaucing = true;
            }
            return;
        }

        if (!abduct)
        {
            Vector3 direction = (saucerSpot.position - transform.position).normalized;
            transform.position += direction * Time.deltaTime * saucerSpeed;
            if (Vector3.Distance(saucerSpot.position, transform.position) < 0.01f)
            {
         
                abduct = true;
                StartCoroutine(SillySound());
           
            }
        }

        if(isAbducting)
        {
            player.position += playerSpeed * Time.deltaTime * Vector3.up;
            if(player.position.y > transform.position.y) { Application.Quit(); }
        }

    }

    IEnumerator SillySound()
    {
        source.Pause();
        source.loop = false;
        source.clip = abductClip;
        source.Play();
        yield return new WaitForSeconds(1f);
        Physics.gravity = Physics.gravity * -abductSpeed;
        spot.gameObject.SetActive(true);
        isAbducting = true;

    }

 
}
