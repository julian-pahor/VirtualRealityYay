using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SinisterFloatingOrb : MonoBehaviour
{

    VisiblityDetector detector;
    public float speed;

    public float minDistance;
    Rigidbody body;
    XRGrabInteractable interactable;
    Marshmallow mallow;

    bool sinister;
    bool mellow;
    
    void Start()
    {
        detector = GetComponent<VisiblityDetector>();
        body = GetComponent<Rigidbody>();
        interactable = GetComponent<XRGrabInteractable>();
        mallow = GetComponent<Marshmallow>();

        body.isKinematic = true;
        interactable.enabled = false;
        mallow.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!mellow)
        {
            if(Vector3.Distance(detector.ViewerPosition(),transform.position) < minDistance)
            {
                mellow = true;
                transform.localScale = new Vector3(.2f, .2f, .2f);
                body.isKinematic = false;
                interactable.enabled = true;
                mallow.enabled = true;
            }

            if (!sinister && detector.IsVisible()) sinister = true;

            if (sinister)
            {
                Vector3 direction = (detector.ViewerPosition() - transform.position).normalized;
                transform.position += direction * speed * Time.deltaTime;
            }
        }
    }
}
