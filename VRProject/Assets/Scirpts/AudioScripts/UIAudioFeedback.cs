using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;
using System;

public class UIAudioFeedback : MonoBehaviour
{
    public AudioClip pickUp;
    public AudioClip putDown;

    private AudioSource source;

    XRGrabInteractable interactable;

    UnityAction<SelectEnterEventArgs> PickUpAction;
    UnityAction<SelectExitEventArgs> PutDownAction;

    // Start is called before the first frame update
    void Start()
    {
        PickUpAction = PickUpEvent;
        PutDownAction = PutDownEvent;
        interactable = GetComponentInParent<XRGrabInteractable>();
        interactable.selectEntered.AddListener(PickUpAction);
        interactable.selectExited.AddListener(PutDownAction);
        source = GetComponent<AudioSource>();
    }

    public void PickUpEvent(SelectEnterEventArgs s)
    {
        source.clip = pickUp;
        source.Play();
    }

    public void PutDownEvent(SelectExitEventArgs s)
    {
        source.clip = putDown;
        source.Play();
    }
}
