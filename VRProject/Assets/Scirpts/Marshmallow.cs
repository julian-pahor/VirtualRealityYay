using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.XR.Interaction.Toolkit;

public class Marshmallow : MonoBehaviour
{

    public ParticleSystem burnEffect;

    [Range(0f, 1f)]
    public float cookThreshold;

    public bool isCooked;

    public float cookRate;

    public float distFromFlame;
    //event to call when the marsh is picked up
    public Action<Marshmallow> onGrab;
    public int mallowIndex;

    public bool onStick;
    [SerializeField]
    float cook;
    private Stick currentStick;

    public AudioClip cookingClip;
    public AudioClip burntClip;
    public AudioClip impactClip;
    public AudioClip eatClip;

    private AudioSource source;

    //--------QUARANTINE----------
    public void SetStick(Stick s)
    {
        currentStick = s;
    }
    //--DANGEROUS LEVELS OF STUPUD--

    Renderer rend;
    Fire fire;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        fire = FindObjectOfType<Fire>();
        source = GetComponent<AudioSource>();
    }
    void Update()
    {

        if(!isCooked)
        {
            if(cook >= cookThreshold)
            {
                burnEffect.Play();
                source.Pause();
                source.loop = false;
                source.clip = burntClip;
                RandomiseAudio();
                source.Play();
                isCooked = true;
            }
        }
       
        //get distance from flame (as %age)
        distFromFlame = fire.CookStrength(transform.position);

        if(distFromFlame > 0.2f && !isCooked)
        {
            source.clip = cookingClip;
            source.loop = true;
            source.volume = Mathf.Clamp(1 * distFromFlame, 0.3f, 0.8f);

            if(!source.isPlaying)
            {
                source.Play();
            }
        }

        //if we're in the flame, cook instantly
        if (distFromFlame >= 1) cook = 1;

        //otherwise, cook at our rate * the proximity to flame
        if(cook < 1)
        {
            cook += cookRate * distFromFlame * Time.deltaTime;
        }

        //update gradient
        rend.material.SetFloat("_CookAmount", cook);

    }


    public void Grab()
    {
        //parent will be unset via the XR Grab Script settings

        if(!onStick)
        {
            return;
        }

        onStick = false;

        Destroy(GetComponent<FixedJoint>());

        currentStick.DetachMallow();
    }

    public void ActivateOnGrab()
    {
        if (onGrab != null)
            onGrab(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(cook > 0.5f)
            {
                //Call any gameplay related feature
                Grab();
                source.Pause();
                source.clip = eatClip;
                RandomiseAudio();
                source.Play();
                Destroy(this.gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        source.Pause();
        source.loop = false;
        source.clip = impactClip;
        RandomiseAudio();
        source.Play();
    }

    private void RandomiseAudio()
    {
        source.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
        source.volume = UnityEngine.Random.Range(0.7f, 0.8f);
    }

    public void PickUp()
    {
        Grab();
    }

    public void PutDown()
    {
        Grab();
    }
}
