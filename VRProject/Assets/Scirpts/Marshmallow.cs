using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marshmallow : MonoBehaviour
{


    public Gradient gradient;
    [Range(0f, 1f)]
    public float cook;
    public float cookRate;

    public float distFromFlame;

    Renderer rend;
    Fire fire;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        fire = FindObjectOfType<Fire>();
    }

    // Update is called once per frame
    void Update()
    {
       
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
}
