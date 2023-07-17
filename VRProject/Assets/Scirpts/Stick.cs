using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.OpenXR.Input;

public class Stick : MonoBehaviour
{
    public Transform stickPoint;

    private Marshmallow currentMallow;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if(currentMallow != null)
        {
            return;
        }

        Marshmallow m;

        if (other.gameObject.TryGetComponent<Marshmallow>(out m))
        {
            if(m.onStick)
            {
                return;
            }
            m.gameObject.transform.position = stickPoint.position;
            m.AddComponent<FixedJoint>().connectedBody = rb;
            m.onStick = true;
            m.SetStick(this);
            currentMallow = m;
        }
    }

    public void DetachMallow()
    {
        currentMallow = null;
    }
}
