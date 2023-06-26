using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;
using UnityEngine.InputSystem.XR;

public class FaceIgniter : MonoBehaviour
{
    Fire fire;
    public ParticleSystem faceFire;
    bool onfire;
    float timer;
    public float flameTime;
    public float immunityTime;

    public ActionBasedController rh;
    public ActionBasedController lh;
    void Start()
    {
     
        fire = FindObjectOfType<Fire>();
        faceFire.Stop();
    }

    // Update is called once per frame
    void Update()
    {

        if(Time.time < immunityTime)
            return;

        if(!onfire && Vector3.Distance(transform.position,fire.transform.position) < fire.flameRadius+.65f)
        {
            onfire = true;
            timer = 0;
            faceFire.Play();
            lh.SendHapticImpulse(1, flameTime);
            rh.SendHapticImpulse(1, flameTime);
        }

        if(onfire)
        {
            timer += Time.deltaTime;
            if(timer > flameTime)
            {
                onfire = false;
                faceFire.Stop();
            }
        }
    }

}
