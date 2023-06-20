using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Marshmallow : MonoBehaviour
{

    public ParticleSystem burnEffect;
    public Gradient gradient;

    [Range(0f, 1f)]
    public float cookThreshold;

    public bool isCooked;

    public float cookRate;

    public float distFromFlame;
    //event to call when the marsh is picked up
    public Action<Marshmallow> onGrab;
    public int mallowIndex;

    public bool onStick;

    float cook;
    private Stick currentStick;

    //--------QUARANTINE----------
    public void SetStick(Stick s)
    {
        currentStick = s;
    }
    //--DANGEROUS LEVELS OF STUPUD--

    public bool set;

    Renderer rend;
    Fire fire;


    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        fire = FindObjectOfType<Fire>();
    }
    void Update()
    {

        if(!isCooked)
        {
            if(cook >= cookThreshold)
            {
                burnEffect.Play();
                isCooked = true;
            }
        }
       
        //get distance from flame (as %age)
        distFromFlame = fire.CookStrength(transform.position);

        //if we're in the flame, cook instantly
        if (distFromFlame >= 1) cook = 1;

        //otherwise, cook at our rate * the proximity to flame
        if(cook < 1)
        {
            cook += cookRate * distFromFlame * Time.deltaTime;
        }

        //update gradient
        rend.material.color = gradient.Evaluate(cook);
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
  
}
