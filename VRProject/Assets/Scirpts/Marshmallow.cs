using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marshmallow : MonoBehaviour
{


    public Gradient gradient;
    [Range(0f, 1f)]
    public float cook;

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
        //gradient.Evaluate(cook);
        rend.material.color = gradient.Evaluate(cook);

        distFromFlame = fire.CookStrength(transform.position);

    }
}
