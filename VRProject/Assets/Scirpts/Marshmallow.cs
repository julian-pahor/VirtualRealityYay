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

    private Stick currentStick;
    public void SetStick(Stick s)
    {
        currentStick = s;
    }

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
        if(Input.GetMouseButtonDown(0))
        {
            onGrab.Invoke(this);
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

    private void OnTriggerEnter(Collider other)
    {
        //Call any gameplay related feature

        Destroy(this.gameObject);
    }

}
