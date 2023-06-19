using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Marshmallow : MonoBehaviour
{


    public Gradient gradient;
    [Range(0f, 1f)]
    public float cook;
    public float cookRate;

    public float distFromFlame;
    //event to call when the marsh is picked up
    public Action<Marshmallow> onGrab;
    public int mallowIndex;

    public bool onStick;

    private float mass;


    private Stick currentStick;
    public void SetStick(Stick s)
    {
        currentStick = s;
    }

    Renderer rend;
    Fire fire;
    float lerp;
    public bool set;
    bool active;
    Vector3 seekLocation;
    Renderer rend;
    Fire fire;
    MarshMallowManager manager;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        fire = FindObjectOfType<Fire>();
        rb = FindObjectOfType<Rigidbody>();
    }


    public void Initialise(Vector3 location, MarshMallowManager mmm)
    {
        rb.isKinematic = true;
        seekLocation = location;
        manager = mmm;
    }

    void Update()
    {
        if (!set)
        {
            lerp += Time.deltaTime;
            Vector3.Lerp(transform.position,seekLocation,lerp);
            if (lerp >= 1) set = true;
        }

        if (!active && set)
        {
            if(Vector3.Distance(transform.position,manager.transform.position) > 1)
            {
                ActivateOnGrab();
                active = true;
                rb.isKinematic = false;
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

        //GetComponent<FixedJoint>().

        currentStick.DetachMallow();
    }

    public void ActivateOnGrab()
    {
        if (onGrab != null)
            onGrab(this);
    }
  
}
